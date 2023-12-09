using ApplicationCore.Utils;
using DevExtreme.AspNet.Mvc;
using Microsoft.Reporting.WebForms;
using Proyecto.Models;
using Proyecto.Permisos;
using Proyecto.Reportes;
using Proyecto.Reportes.DataSetPedidosTableAdapters;
using Proyecto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        #region vistas
        /// <summary>
        /// Acción para la página principal de pedidos, accesible solo por empleados.
        /// </summary>
        [PermisosRol(RolesPermisos.Empleados, RolesPermisos.Administrador)]
        public ActionResult Index()
        {
            try
            {
                Log.Info("Accediendo a la página de pedidos");
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex,MethodBase.GetCurrentMethod());
                return RedirectToAction("Error", "Acceso");
            }
        }

        /// <summary>
        /// Acción para mostrar los pedidos del cliente.
        /// </summary>
        public ActionResult PedidosCliente()
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["usuario"];

            ViewBag.id = usuario.IDUsuario;
            return View();
        }
        #endregion

        #region reportes
        /// <summary>
        /// Acción para generar un reporte en formato PDF basado en un ID de pedido.
        /// </summary>
        /// <param name="idpedido">ID del pedido para generar el reporte.</param>
        /// <returns>Archivo PDF del reporte del pedido.</returns>
        [HttpGet]
        public ActionResult ReportePedidos(int idpedido)
        {
            // Declaración de variables para el reporte
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

                


                // Información de registro: inicio de generación del reporte para el pedido específico
                Log.Info($"Generando reporte en PDF para el pedido con ID: {idpedido}");

                // Creación del dataset y adaptador para llenar los datos del pedido
                DataSetPedidos dataSetPedidos = new DataSetPedidos();
                PedidosTableAdapter pedidosTable = new PedidosTableAdapter();

                // Llenar el dataset con los datos del pedido especificado por su ID
                pedidosTable.Fill(dataSetPedidos.Pedidos, idpedido);
                rptPath = Server.MapPath("~/Reportes/ReportePedidos.rdlc");

                // Convertir los datos a una lista para el reporte
                var listaPedidos = dataSetPedidos.Pedidos.ToList();

                // Verificar si existen datos en el reporte, si no hay, redirigir a la página principal
                if (listaPedidos.Count == 0)
                {
                    ViewBag.message = "No existen Facturas en estos Lapsos de tiempo";
                    return View("Index");
                }

                // Configurar el DataSource del ReportViewer
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


                ReportParameter param = new ReportParameter("montosubtotalletras", "El monto total es: " + MonedaEnLetras.Convertir(montosubtotal.ToString(), true, "Colones", " Centimos"));
                rv.LocalReport.SetParameters(param);

                ReportParameter paramDolar = new ReportParameter("MontoDolar", precioDolar.ToString());
                rv.LocalReport.SetParameters(paramDolar);

                


                rv.LocalReport.Refresh();
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

                // Generar el archivo PDF del reporte del pedido
                streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                // Registro: Reporte generado con éxito para el pedido con ID específico
                Log.Info($"Se generó el reporte en PDF para el pedido con ID: {idpedido}");

                // Devolver el archivo PDF como resultado de la acción
                return File(streamBytes, "application/pdf", $"Pedido_{idpedido}.pdf");
            }
            catch (Exception ex)
            {
                // En caso de error, registrar la excepción y redirigir a una página de error
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Error", "Acceso");
            }
        }
        #endregion

        #region Metodo de vista para traduccion del DevExtreme
        /// <summary>
        /// Acción para proporcionar datos de localización para DevExtreme.
        /// </summary>
        /// <returns>Datos de localización para DevExtreme.</returns>
        public ActionResult CldrData()
        {
            return new CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("es")
                .UseLocales("es", "es")
                .Build();
        }
        #endregion
    }
}