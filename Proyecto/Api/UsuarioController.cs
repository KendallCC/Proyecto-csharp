using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Proyecto.Api
{
    [Route("api/usuario/{action}", Name = "usuarios")]
    public class UsuarioController : ApiController
    {
        // Instancia del repositorio de usuarios
        IRepositorioUsuarios usuario = new RepositorioUsuarios();

        #region Crud Usuarios
        /// <summary>
        /// Obtiene una lista de todos los usuarios.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de todos los usuarios.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(usuario.ObtenerTodosLosUsuarios(), loadOptions));
        }

        /// <summary>
        /// Inserta un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="form">Datos del nuevo usuario a insertar.</param>
        /// <returns>Respuesta HTTP que indica el éxito o fracaso de la operación.</returns>
        [HttpPost]
        public HttpResponseMessage Insertar(FormDataCollection form)
        {
            var values = form.Get("values");
            var Usuario = new Usuario();
            JsonConvert.PopulateObject(values, Usuario);
            Validate(Usuario);

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
            usuario.AgregarUsuario(Usuario);
            return Request.CreateResponse(HttpStatusCode.Created, Usuario);
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="form">Datos actualizados del usuario.</param>
        /// <returns>Respuesta HTTP que indica el éxito o fracaso de la operación.</returns>
        [HttpPut]
        public HttpResponseMessage Actualizar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var User = usuario.ObtenerUsuarioPorId(key);
            JsonConvert.PopulateObject(values, User);

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
            usuario.ActualizarUsuario(User);
            return Request.CreateResponse(HttpStatusCode.OK, User);
        }

        /// <summary>
        /// Elimina un usuario existente.
        /// </summary>
        /// <param name="form">Identificador del usuario a borrar.</param>
        [HttpDelete]
        public void Borrar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            usuario.EliminarUsuario(key);
        }
        #endregion
    }
}
