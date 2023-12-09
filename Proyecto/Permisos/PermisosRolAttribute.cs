using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Permisos
{
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        private List<RolesPermisos> roles;

        public PermisosRolAttribute(params RolesPermisos[] _roles)
        {
            roles = _roles.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"] != null)
            {
                Usuario usuario = HttpContext.Current.Session["usuario"] as Usuario;

                if (!roles.Contains((RolesPermisos)usuario.RoleID))
                {
                    filterContext.Result = new RedirectResult("~/Acceso/Sinpermiso");
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
