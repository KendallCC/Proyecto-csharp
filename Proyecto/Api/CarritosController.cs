using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Proyecto.Interfaces;
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
    [Route("api/Carritos/{action}", Name = "Carritos")]
    public class CarritosController : ApiController
    {
        // Instancia del repositorio de Carrito
        IRepositorioCarrito Carrito = new RepositorioCarrito();

        #region api con el Crud de Carrito
        /// <summary>
        /// Obtiene una lista paginada de elementos de carrito asociados a un usuario.
        /// </summary>
        /// <param name="idUsuario">ID del usuario para obtener su carrito.</param>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista paginada de elementos de carrito.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(int idUsuario, DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(Carrito.ObtenerItemsCarritoPorUsuario(idUsuario), loadOptions)
            );
        }

        /// <summary>
        /// Actualiza un elemento del carrito.
        /// </summary>
        /// <param name="form">Datos a actualizar.</param>
        /// <returns>Respuesta HTTP que indica el resultado de la operación.</returns>
        [HttpPut]
        public HttpResponseMessage Actualizar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var carrito = Carrito.ObtenerItemCarritoPorId(key);

            // Actualiza el objeto carrito con los valores recibidos
            JsonConvert.PopulateObject(values, carrito);

            // Verifica si hay errores de validación
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

            // Calcula el total del carrito después de la actualización
            carrito.Total = carrito.Cantidad * carrito.Producto.Precio;
            Carrito.ActualizarItemCarrito(carrito);

            return Request.CreateResponse(HttpStatusCode.OK, carrito);
        }

        /// <summary>
        /// Elimina un elemento del carrito.
        /// </summary>
        /// <param name="form">Datos para realizar la eliminación.</param>
        [HttpDelete]
        public void Borrar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            Carrito.EliminarItemCarrito(key);
        }
        #endregion

    }
}
