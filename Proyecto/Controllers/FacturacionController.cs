using DevExtreme.AspNet.Mvc;
using Microsoft.Reporting.WebForms;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Permisos;
using Proyecto.Reportes;
using Proyecto.Reportes.DataSetFacturacionTableAdapters;
using Proyecto.Reportes.DataSetReporteMensualTableAdapters;
using Proyecto.Repositories;
using Proyecto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class FacturacionController : Controller
    {

        #region Vistas

        /// <summary>
        /// Acción para mostrar la vista principal de facturación.
        /// </summary>
        /// <returns>Vista principal de facturación.</returns>
        [PermisosRol(RolesPermisos.Empleados, RolesPermisos.Administrador)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acción para mostrar el reporte por fecha.
        /// </summary>
        /// <returns>Vista para generar el reporte por fecha.</returns>
        [PermisosRol(RolesPermisos.Empleados, RolesPermisos.Administrador)]
        public ActionResult ReportePorFecha()
        {
            return View();
        }



        [PermisosRol(RolesPermisos.Administrador)]
        public ActionResult GraficoVentas()
        {
            IRepositorioFacturas facturas = new RepositorioFacturas();
            var lista = facturas.ObtenerFacturas();

            // Suponiendo que tienes una lista de facturas
            DateTime fechaActual = DateTime.Now;
            int mesActual = fechaActual.Month;
            int añoActual = fechaActual.Year;

            // Filtrar las facturas del mes actual
            var facturasMesActual = lista.Where(factura =>
                factura.FechaFactura.Value.Month == mesActual && factura.FechaFactura.Value.Year == añoActual
            );

            // Obtener la cantidad de usuarios únicos
            var cantidadUsuarios = facturasMesActual.Select(factura => factura.Pedido.Usuario.IDUsuario).Distinct().Count();

            // Contar la cantidad total de facturas
            var cantidadFacturas = facturasMesActual.Count();


            ViewBag.CantidadUsuarios = cantidadUsuarios; // Pasar la cantidad de usuarios a la vista
            ViewBag.CantidadFacturas = cantidadFacturas; // Pasar la cantidad de facturas a la vista





            return View();
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

        #region Acciones de generar reportes
        /// <summary>
        /// Acción para generar el reporte de facturas.
        /// </summary>
        /// <param name="form">Datos del formulario.</param>
        /// <returns>Archivo PDF con el reporte de facturas.</returns>
        [HttpPost]
        public ActionResult ReporteFacturas(FormCollection form)
        {
            // Variables de configuración para el reporte
            string rptPath = "";
            ReportViewer rv = new ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string deviceInfo = "";
            string fechainicio = "";
            string fechafin = "";

            try
            {
                // Obtener fechas del formulario
                fechainicio = form["txtFiltro1"].ToString();
                fechafin = form["txtFiltro2"].ToString();

                // Verificar si ambas fechas están presentes
                if (string.IsNullOrEmpty(fechainicio) || string.IsNullOrEmpty(fechafin))
                {
                    ViewBag.message = "Los 2 parámetros son requeridos";
                    return View("ReportePorFecha");
                }

                // Acceso a la base de datos para obtener los datos del reporte
                DataSetFacturacion dSFacturacion = new DataSetFacturacion();
                FacturaTableAdapter facturaTable = new FacturaTableAdapter();
                facturaTable.Fill(dSFacturacion.Factura, fechainicio, fechafin);
                rptPath = Server.MapPath("~/Reportes/ReporteFacturacion.rdlc");
                var listaFactura = dSFacturacion.Factura.ToList();

                // Verificar si hay datos para generar el reporte
                if (listaFactura.Count == 0)
                {
                    ViewBag.message = "No existen Facturas en estos lapsos de tiempo";
                    return View("ReportePorFecha");
                }

                // Configuración del reporte y renderización en formato PDF
                ReportDataSource rds = new ReportDataSource("DataSetEncabezadoFactura", listaFactura);
                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;
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
                streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                Log.Info($"Se genera el reporte entre las fechas {fechainicio} y {fechafin} ");
                // Retornar el archivo PDF generado
                return File(streamBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción ocurrida durante el proceso
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // redirigir a la pagina de error en caso de haber un problema
                return RedirectToAction("Error", "Acceso");
            }
        }


        /// <summary>
        /// Acción para generar el reportel mes.
        /// </summary>
        /// <returns>Archivo PDF con el reporte del mes </returns>
        [HttpPost]
        public ActionResult ReporteMensual()
        {
            // Variables de configuración para el reporte
            string rptPath = "";
            ReportViewer rv = new ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string deviceInfo = "";

            try
            {
                DataSetReporteMensual dSReportemensual = new DataSetReporteMensual();
                UsuarioTableAdapter usuarioTable = new UsuarioTableAdapter();
                CategoriaTableAdapter categoriaTable = new CategoriaTableAdapter();
                RolUsuarioTableAdapter rolUsuarioTable = new RolUsuarioTableAdapter();
                ProductoTableAdapter productoTable = new ProductoTableAdapter();
                EstadoPedidoTableAdapter estadoPedidoTable = new EstadoPedidoTableAdapter();

                usuarioTable.Fill(dSReportemensual.Usuario);
                categoriaTable.Fill(dSReportemensual.Categoria);
                rolUsuarioTable.Fill(dSReportemensual.RolUsuario);
                productoTable.Fill(dSReportemensual.Producto);
                estadoPedidoTable.Fill(dSReportemensual.EstadoPedido);

                var listaUsuarios = dSReportemensual.Usuario.ToList();
                var listaCategoria = dSReportemensual.Categoria.ToList();
                var listaRoles = dSReportemensual.RolUsuario.ToList();
                var listaProducto = dSReportemensual.Producto.ToList();
                var listaEstadoProducto = dSReportemensual.EstadoPedido.ToList();


                rptPath = Server.MapPath("~/Reportes/ReporteMensual.rdlc");

                // Configuración del reporte y renderización en formato PDF
                ReportDataSource rdsUsuario = new ReportDataSource("DataSetMensual", listaUsuarios);
                ReportDataSource rdsCategoria = new ReportDataSource("DataSetCategorias", listaCategoria);
                ReportDataSource rdsRoles = new ReportDataSource("DataSetRoles", listaRoles);
                ReportDataSource rdsProducto = new ReportDataSource("DataSetProducto", listaProducto);
                ReportDataSource rdsEstadoPedido = new ReportDataSource("DataSetEstados", listaEstadoProducto);

                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rdsUsuario);
                rv.LocalReport.DataSources.Add(rdsCategoria);
                rv.LocalReport.DataSources.Add(rdsRoles);
                rv.LocalReport.DataSources.Add(rdsProducto);
                rv.LocalReport.DataSources.Add(rdsEstadoPedido);
                rv.LocalReport.EnableHyperlinks = true;
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
                streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                Log.Info($"Se genera el reporte Mensual");
                // Retornar el archivo PDF generado
                return File(streamBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción ocurrida durante el proceso
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // redirigir a la pagina de error en caso de haber un problema
                return RedirectToAction("Error", "Acceso");
            }
        }

        #endregion




    }
}
