using Proyecto.Models;
using Proyecto.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        #region Vista
        /// <summary>
        /// Acción para mostrar la vista principal de categorías (solo para empleados).
        /// </summary>
        /// <returns>Vista principal de categorías.</returns>
        [PermisosRol(RolesPermisos.Empleados,RolesPermisos.Administrador)]
        public ActionResult Index()
        {
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
            return new DevExtreme.AspNet.Mvc.CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("es")
                .UseLocales("es", "es")
                .Build();
        }
        #endregion
    }
}