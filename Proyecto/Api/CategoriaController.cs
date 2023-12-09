using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Proyecto.Api
{
    [Route("api/Categorias/{action}", Name = "Categorias")]
    public class CategoriaController : ApiController
    {
        // Instancia del repositorio de Categorías
        IRepositorioCategorias Categorias = new RepositorioCategorias();

        #region Crud Categorias
        /// <summary>
        /// Obtiene una lista de todas las categorías.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de todas las categorías.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(Categorias.ObtenerTodasLasCategorias(), loadOptions)
            );
        }

        /// <summary>
        /// Inserta una nueva categoría.
        /// </summary>
        /// <param name="form">Datos de la nueva categoría.</param>
        /// <returns>Respuesta HTTP que indica el resultado de la operación.</returns>
        [HttpPost]
        public HttpResponseMessage Insertar(FormDataCollection form)
        {
            var values = form.Get("values");

            // Crear un nuevo objeto de categoría y poblarlo con los valores recibidos
            var categoria = new Categoria();
            JsonConvert.PopulateObject(values, categoria);

            // Validar el modelo de categoría
            Validate(categoria);

            // Verificar si hay errores de validación
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

            // Agregar la nueva categoría
            Categorias.AgregarCategoria(categoria);
            return Request.CreateResponse(HttpStatusCode.Created, categoria);
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        /// <param name="form">Datos actualizados de la categoría.</param>
        /// <returns>Respuesta HTTP que indica el resultado de la operación.</returns>
        [HttpPut]
        public HttpResponseMessage Actualizar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var categoria = Categorias.ObtenerCategoriaPorId(key);

            // Actualiza la categoría con los nuevos valores recibidos
            JsonConvert.PopulateObject(values, categoria);

            // Verificar si hay errores de validación
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

            // Actualizar la categoría
            Categorias.ActualizarCategoria(categoria);
            return Request.CreateResponse(HttpStatusCode.OK, categoria);
        }

        /// <summary>
        /// Elimina una categoría existente.
        /// </summary>
        /// <param name="form">Datos para realizar la eliminación.</param>
        [HttpDelete]
        public void Borrar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));

            // Eliminar la categoría por su ID
            Categorias.EliminarCategoria(key);
        }
        #endregion
    }
}
