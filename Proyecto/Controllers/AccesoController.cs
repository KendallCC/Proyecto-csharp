using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Repositories;
using Proyecto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Proyecto.Controllers
{
    public class AccesoController : Controller
    {

        #region Vistas

        // Vista de login para ingresar a la vista
        /// <summary>
        /// Método que devuelve la vista principal.
        /// </summary>
        /// <returns>ActionResult que representa la vista principal.</returns>
        public ActionResult Index()
        {
            return View();
        }

        // Vista de Registro para que el usuario se registre
        /// <summary>
        /// Método que devuelve la vista de registro de usuarios.
        /// </summary>
        /// <returns>ActionResult que representa la vista de registro.</returns>
        public ActionResult Registro()
        {
            return View();
        }

        // Vista para cuando un usuario intenta accesar accesa a una funcionalidad no permitida por su rol
        /// <summary>
        /// Método que devuelve la vista de registro de usuarios.
        /// </summary>
        /// <returns>ActionResult que representa la vista de registro.</returns>
        public ActionResult Sinpermiso()
        {
            return View();
        }
        // Vista de error para cuando la pagina se cae en el entorno de trabajo
        /// <summary>
        /// Método que devuelve la vista de error.
        /// </summary>
        /// <returns>ActionResult que representa la vista de error.</returns>
        public ActionResult Error()
        {
            return View();
        }
        #endregion

        #region Acciones
        /// <summary>
        /// Acción POST que registra un nuevo usuario en la aplicación.
        /// </summary>
        /// <param name="Nombre">El nombre del nuevo usuario.</param>
        /// <param name="CorreoElectronico">El correo electrónico del nuevo usuario.</param>
        /// <param name="Contraseña">La contraseña del nuevo usuario.</param>
        /// <param name="confirmarContraseña">La confirmación de la contraseña del nuevo usuario.</param>
        /// <returns>
        /// - Redirige a la vista de registro si hay errores en el proceso de registro.
        /// - Redirige a la página de inicio de sesión si el registro es exitoso.
        /// - Redirige a la página de error si ocurre alguna excepción durante el proceso.
        /// </returns>
        [HttpPost]
        public ActionResult Registrar(string Nombre, string CorreoElectronico, string Contraseña, string confirmarContraseña)
        {
            try
            {
                // Verificar si las contraseñas coinciden
                if (Contraseña != confirmarContraseña)
                {
                    ViewData["messager"] = "Contraseñas no coinciden";
                    return View("Registro");
                }

                // Verificar si se han completado todos los campos
                if (!ModelState.IsValid)
                {
                    ViewData["messager"] = "Por favor rellene todos los campos";
                    return View("Registro");
                }

                // Crear un nuevo objeto de Usuario con la información proporcionada
                var usuario = new Usuario()
                {
                    Contraseña = Contraseña,
                    Nombre = Nombre,
                    CorreoElectronico = CorreoElectronico,
                    RoleID = (int)RolesPermisos.Cliente,
                    Estado = "Activo"
                };

                // Acceder al repositorio de usuarios
                IRepositorioUsuarios usuarios = new RepositorioUsuarios();

                // Verificar si el correo electrónico ya está registrado
                if (usuarios.ObtenerUsuarioPorcorreo(CorreoElectronico))
                {
                    ViewData["messager"] = "Correo electrónico ya registrado";
                    return View("Registro");
                }
                // Registrar al usuario en el Log
                Log.Info($"Se agrega el usuario {usuario.Nombre} al sistema");
               // Agregar el nuevo usuario al repositorio
               usuarios.AgregarUsuario(usuario);
            }
            catch (Exception ex)
            {
                // Registrar cualquier excepción ocurrida durante el proceso
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // redirigir a la pagina de error en caso de haber un problema
                return RedirectToAction("Error", "Acceso");
            }

            // Redirigir a la página de inicio de sesión después de un registro exitoso
            return RedirectToAction("Index", "Acceso");
        }

        /// <summary>
        /// Método HTTP POST para iniciar sesión de usuarios.
        /// </summary>
        /// <param name="Contraseña">La contraseña del usuario.</param>
        /// <param name="correo">El correo electrónico del usuario.</param>
        /// <returns>ActionResult que redirige a diferentes vistas según el resultado del inicio de sesión.</returns>
        [HttpPost]
        public ActionResult login(string Contraseña, string correo)
        {
            try
            {
                // Inicialización de la variable de mensaje en la vista
                ViewBag.mensaje = null;

                // Acceso al repositorio de usuarios
                IRepositorioUsuarios usuarios = new RepositorioUsuarios();

                // Autenticación del usuario
                var usuario = usuarios.AutenticarUsuario(correo, Contraseña);

                // Verificar si la autenticación fue exitosa
                if (usuario != null)
                {
                    // Establecer la cookie de autenticación
                    FormsAuthentication.SetAuthCookie(usuario.CorreoElectronico, false);

                    // Almacenar información del usuario en la sesión
                    Session["usuario"] = usuario;
                    Session["nombreusuario"] = usuario.Nombre;
                    Session["rol"] = usuario.RolUsuario.Rol;

                    // Redireccionar según el rol del usuario
                    if (usuario.RoleID == (int)RolesPermisos.Administrador)
                    {
                        Log.Info($"Administrador {usuario.Nombre} ingresa a Roles");
                        return RedirectToAction("Index", "Roles");
                    }
                    else if (usuario.RoleID == (int)RolesPermisos.Empleados)
                    {
                        Log.Info($"Empleado {usuario.Nombre} ingresa a Productos");
                        return RedirectToAction("Index", "Productos");
                    }
                    else if (usuario.RoleID == (int)RolesPermisos.Cliente)
                    {
                        Log.Info($"Cliente {usuario.Nombre} ingresa al catálogo");
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Mostrar mensaje de error si la autenticación falla
                ViewBag.mensaje = "El Correo o la Contraseña no coincide";
                return View("Index");
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
        /// Método para cerrar sesión de usuario.
        /// </summary>
        /// <returns>ActionResult que redirige a la vista principal después del cierre de sesión.</returns>
        public ActionResult logout()
        {

            // Registrar la acción de deslogueo del usuario
            Log.Info($"Usuario se desloguea de la Aplicación");

            // Cerrar la sesión del usuario y eliminar la cookie de autenticación
            FormsAuthentication.SignOut();

            // Limpiar la información de usuario en la sesión
            Session["usuario"] = null;

            // Redirigir a la vista principal
            return View("index");
        }
        #endregion


    }
}