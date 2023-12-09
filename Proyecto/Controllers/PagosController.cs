using ApplicationCore.Utils;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Reportes;
using Proyecto.Reportes.DataSetPedidosTableAdapters;
using Proyecto.Repositories;
using Proyecto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Utilitarios;

namespace Proyecto.Controllers
{
    [Authorize]
    public class PagosController : Controller
    {
        #region Vistas
        /// <summary>
        /// Acción para mostrar la página de pagos.
        /// </summary>
        /// <returns>
        /// Redirige a la página principal si no hay productos en el carrito;
        /// de lo contrario, muestra la vista de pagos.
        /// </returns>
        public ActionResult Index()
        {
            // Obtener el carrito del usuario
            IRepositorioCarrito carrito = new RepositorioCarrito();

            var user = Session["usuario"] as Usuario;

            int idUsuario = user.IDUsuario;

            int cantidad = carrito.ObtenerItemsCarritoPorUsuario(idUsuario).Count();

            // Redirigir a la página principal si no hay productos en el carrito
            if (cantidad == 0)
            {
                return RedirectToAction("Index", "Home");
            };

            // Mostrar la vista de pagos
            return View();
        }
        #endregion

        #region Acciones

        /// <summary>
        /// Procesa el pago de un pedido utilizando los detalles de una tarjeta de crédito y genera una factura para el usuario.
        /// </summary>
        /// <param name="numeracion">Número de la tarjeta de crédito.</param>
        /// <param name="FechaVencimiento">Fecha de vencimiento de la tarjeta.</param>
        /// <param name="Seguridad">Código de seguridad de la tarjeta.</param>
        /// <returns>ActionResult que indica el resultado del proceso de pago.</returns>
        [HttpPost]
        public ActionResult Cancelar(string numeracion, DateTime FechaVencimiento, int Seguridad)
        {
            try
            {
                var user = Session["usuario"] as Usuario;
                int Idusuario = user.IDUsuario;
                decimal? totalPagar = 0;
                string montototal = "0";
                Tarjeta tarjeta = new Tarjeta()
                {
                    numeracion = numeracion,
                    FechaVencimiento = FechaVencimiento,
                    Seguridad = Seguridad
                };

                // Verificar si el modelo es válido
                if (!ModelState.IsValid)
                {
                    // Retorna un código de estado HTTP de error y un mensaje indicando la falla
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se ha podido pagar su pedido");
                }

                // Obtener los elementos en el carrito del usuario
                IRepositorioCarrito carritoCompras = new RepositorioCarrito();
                var lista = carritoCompras.ObtenerItemsCarritoPorUsuario(Idusuario);

                // Crear un pedido para el usuario
                Pedido pedido = new Pedido()
                {
                    IDUsuario = Idusuario,
                    FechaPedido = DateTime.UtcNow,
                    EstadoID = 1
                };

                // Repositorios para manejar pedidos, detalles de pedido y facturas
                IRepositorioPedidos pedidos = new RepositorioPedidos();
                IRepositorioDetallesPedido detalles = new RepositorioDetallesPedido();
                IRepositorioFacturas facturas = new RepositorioFacturas();

                // Realizar un nuevo pedido y obtener su ID
                int idInsertado = pedidos.RealizarPedido(pedido);

                Factura factura = new Factura()
                {
                    IDPedido = idInsertado,
                    FechaFactura = pedido.FechaPedido,
                    DetallesFactura = "Pago con tarjeta"
                };

                // Crear los detalles del pedido
                foreach (var item in lista)
                {
                    DetallePedido detallePedido = new DetallePedido()
                    {
                        IDPedido = idInsertado,
                        IDProducto = item.IDProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.Producto.Precio,
                        Subtotal = item.Total
                    };
                    totalPagar += detallePedido.Subtotal;
                    detalles.AgregarDetallePedido(detallePedido);
                }

                // Limpiar el carrito del usuario después de completar la compra
                carritoCompras.EliminarCarritoUsuario(Idusuario);

                // Generar la factura del pedido realizado
                facturas.GenerarFactura(factura);

                // Generar un archivo PDF del reporte del pedido
                byte[] pdfBytes = ReportePedidos(idInsertado);

                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    //validar si totalpagar tiene algo
                    if (totalPagar.HasValue)
                    {
                        montototal = totalPagar.Value.ToString("#,0.###"); // Aplicando formato
                    }

                    //Estilos del correo
                    string estilos = @"<style>
    body {
      font-family: Arial, sans-serif;
      margin: 0;
      padding: 20px;
      background-color: #f4f4f4;
    }
    .invoice {
      background-color: #fff;
      border-radius: 8px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
      padding: 30px;
      max-width: 600px;
      margin: 0 auto;
    }
    .invoice-header {
      text-align: center;
      margin-bottom: 20px;
    }
    .invoice-header h1 {
      font-size: 32px;
      color: #333;
    }
    .invoice-details {
      margin-bottom: 30px;
    }
    .invoice-details p {
      margin: 5px 0;
      color: #555;
    }
    .invoice-total {
      text-align: right;
    }
    .invoice-total p {
      font-weight: bold;
      font-size: 18px;
      margin: 5px 0;
    }
    .thank-you {
      text-align: center;
      margin-top: 20px;
      color: #777;
    }
  </style>";

                    string body = $@"
<!DOCTYPE html>
<html lang=""es"">
<head>
  <meta charset=""UTF-8"">
  <title>Factura de TechStore</title>
  {estilos}
</head>
<body>

<div class=""invoice"">
  <div class=""invoice-header"">
    <h1>Factura de TechStore</h1>
  </div>
  <div class=""invoice-details"">
    <p><strong>Cliente:</strong> {user.Nombre}</p>
    <p><strong>Forma de pago:</strong> {factura.DetallesFactura}</p>
    <p><strong>Fecha:</strong> {factura.FechaFactura.Value.ToLongDateString()}</p>
  </div>

  <div class=""invoice-total"">
    <p><strong>Total:</strong> ₡ {montototal}</p>
  </div>
  <div class=""thank-you"">
    <p>¡Gracias por tu compra!</p>
    <p><strong>Adjuntamos tu factura en pdf junto con este correo</strong></strong></p>
  </div>
</div>
</body>
</html>
";



                    // Enviar el reporte generado por correo electrónico
                    EnviarCorreo enviarCorreo = new EnviarCorreo();
                    enviarCorreo.enviarCorreoGmail(body, user.CorreoElectronico, "Reporte de Pedido", pdfBytes, $"Pedido_{pedido.IDPedido}.pdf");

                    // Mostrar un mensaje de éxito después de enviar el correo
                    TempData["SuccessMessage"] = "Pago procesado exitosamente";
                }
                else
                {
                    // Mostrar un mensaje de error si no se generó el PDF
                    TempData["ErrorMessage"] = "Error al procesar el pago";
                }
                Log.Info($"Se genera el pago para el usuario: {user.Nombre}");
                return View("Index");
            }
            catch (Exception ex)
            {
                // Log para registrar cualquier excepción ocurrida durante el preceso
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Error", "Acceso");
            }
        }
        #endregion

        #region Metodo de vista para traduccion del DevExtreme
        /// <summary>
        /// Método para obtener los datos de localización para DevExtreme.
        /// </summary>
        /// <returns>DevExtreme localization data.</returns>
        public ActionResult CldrData()
        {
            return new DevExtreme.AspNet.Mvc.CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("es")
                .UseLocales("es", "es")
                .Build();
        }
        #endregion

        #region Reportes 
        /// <summary>
        /// Método privado para generar el reporte de pedidos en formato PDF.
        /// </summary>
        /// <param name="idpedido">ID del pedido para generar el reporte.</param>
        /// <returns>Bytes del archivo PDF generado.</returns>
        private byte[] ReportePedidos(int idpedido)
        {
            string rptPath = "";
            ReportViewer rv = new ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string deviceInfo = "";
            ServiceBCCR serviceBCCR = new ServiceBCCR();
            double precioDolar = serviceBCCR.GetMontoVentaHoy();
            try
            {
                

                // Crear un nuevo dataset para los pedidos
                DataSetPedidos dataSetPedidos = new DataSetPedidos();
                PedidosTableAdapter pedidosTable = new PedidosTableAdapter();

                // Llenar el dataset con los datos del pedido específico
                pedidosTable.Fill(dataSetPedidos.Pedidos, idpedido);
                rptPath = Server.MapPath("~/Reportes/ReportePedidos.rdlc");

                // Obtener la lista de pedidos
                var listaPedidos = dataSetPedidos.Pedidos.ToList();

                // Verificar si hay pedidos en el rango de tiempo especificado
                if (listaPedidos.Count == 0)
                {
                    ViewBag.message = "No existen Facturas en estos Lapsos de tiempo";
                    return null;
                }
                
                // Agregar la fuente de datos al reporte
                ReportDataSource rds = new ReportDataSource("DataSetPedidos", listaPedidos);
                var montosubtotal = listaPedidos.Sum(pedidos => pedidos.Subtotal);


                string textoqr = $"Su pedido Numero de pedido es:{idpedido} y el monto es de: {montosubtotal}";
                Image imagen = QuickResponse.QuickResponseGenerador(textoqr, 53);
                imagen.Save(@"c:\temp\qr.png", ImageFormat.Png);

                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;

                // Config imagen del QR
                string ruta = @"file:///" + @"C:/TEMP/qr.png";
                // Habilitar imagenes externas
                rv.LocalReport.EnableExternalImages = true;

                ReportParameter ParamreporteQr = new ReportParameter("reporteQr", ruta);
                rv.LocalReport.SetParameters(ParamreporteQr);



                ReportParameter param = new ReportParameter("montosubtotalletras","El monto total es: "+ MonedaEnLetras.Convertir(montosubtotal.ToString(), true, "Colones", " Centimos"));
                rv.LocalReport.SetParameters(param);

                ReportParameter paramDolar = new ReportParameter("MontoDolar", precioDolar.ToString());
                rv.LocalReport.SetParameters(paramDolar);

                
                rv.LocalReport.Refresh();



                // Configuración del dispositivo para el renderizado del PDF
                deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "  <EmbedFonts>None</EmbedFonts>" +
                "</DeviceInfo>";

                // Generar el archivo PDF
                streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                Log.Info($"se genera un reporte de los pedidos para el pedido con id: {idpedido}");
                return streamBytes;

            }
            catch (Exception ex)
            {
                // Log para registrar cualquier excepción ocurrida durante la adición al carrito
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return null;
            }
        }
        #endregion

    }
}
