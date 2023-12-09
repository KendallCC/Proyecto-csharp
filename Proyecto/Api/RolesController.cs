using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Repositories;
using Proyecto.Validation;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Proyecto.Api
{
    [Route("api/Roles/{action}", Name = "Roles")]
    public class RolesController : ApiController
    {
        // Instancia del repositorio de roles
        IRepositorioRoles roles = new RepositorioRoles();

        #region Crud para roles
        /// <summary>
        /// Obtiene una lista de todos los roles.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de todos los roles.</returns>
        [HttpGet]
        public HttpResponseMessage ListarRoles(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(roles.ObtenerTodosLosRoles(), loadOptions));
        }

        /// <summary>
        /// Inserta un nuevo rol en el sistema.
        /// </summary>
        /// <param name="form">Datos del nuevo rol a insertar.</param>
        /// <returns>Respuesta HTTP que indica el éxito o fracaso de la operación.</returns>
        [HttpPost]
        public HttpResponseMessage InsertarRol(FormDataCollection form)
        {
            var values = form.Get("values");
            var rol = new RolUsuario();
            JsonConvert.PopulateObject(values, rol);
            Validate(rol);

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
            roles.AgregarRol(rol);
            return Request.CreateResponse(HttpStatusCode.Created, rol);
        }

        /// <summary>
        /// Actualiza un rol existente.
        /// </summary>
        /// <param name="form">Datos actualizados del rol.</param>
        /// <returns>Respuesta HTTP que indica el éxito o fracaso de la operación.</returns>
        [HttpPut]
        public HttpResponseMessage Actualizar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var rol = roles.ObtenerRolPorId(key);
            JsonConvert.PopulateObject(values, rol);

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }
            roles.ActualizarRol(rol);
            return Request.CreateResponse(HttpStatusCode.OK, rol);
        }

        /// <summary>
        /// Borra un rol existente.
        /// </summary>
        /// <param name="form">Identificador del rol a borrar.</param>
        [HttpDelete]
        public void BorrarRol(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            roles.EliminarRol(key);
        }
        #endregion
    }
}
