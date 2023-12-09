using Proyecto.Interfaces;
using Proyecto.Models;
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
    public class CarritoController : Controller
    {
        // Acción para mostrar el contenido del carrito

        #region Vistas
        /// <summary>
        /// Acción para mostrar el contenido del carrito del usuario.
        /// </summary>
        /// <returns>Vista que muestra el contenido del carrito.</returns>
        public ActionResult Index()
        {
            try
            {
                // Instancia del repositorio del carrito
                IRepositorioCarrito carrito = new RepositorioCarrito();

                // Obtener el usuario de la sesión
                var user = Session["usuario"] as Usuario;

                // Obtener el ID del usuario
                int idUsuario = user.IDUsuario;

                // Configurar variables para la vista
                ViewBag.idUsuario = idUsuario;
                ViewBag.hascarrito = carrito.ObtenerItemsCarritoPorUsuario(idUsuario).Count() > 0;

                return View();
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

        #region Metodo de vista para traduccion del DevExtreme
        /// <summary>
        /// Acción para proporcionar datos de localización para DevExtreme.
        /// </summary>
        /// <returns>Datos de localización para DevExtreme.</returns>
        public ActionResult CldrData()
        {
            return new DevExtreme.AspNet.Mvc.CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("es")
                .UseLocales("es", "es")
                .Build();
        }
        #endregion

    }
}