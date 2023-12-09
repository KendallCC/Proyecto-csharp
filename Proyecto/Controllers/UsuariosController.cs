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
    public class UsuariosController : Controller
    {
        #region vista
        /// <summary>
        /// obtiene la vista de Usuarios para el administrador
        /// </summary>
        /// <returns>Action result con la pagina para el Administrador</returns>
        [PermisosRol(RolesPermisos.Administrador)]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Metodo de vista para traduccion del DevExtreme
        /// <summary>
        /// Obtiene los datos CLDR para internacionalización en la interfaz de usuario.
        /// </summary>
        /// <returns>Script con los datos CLDR.</returns>
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