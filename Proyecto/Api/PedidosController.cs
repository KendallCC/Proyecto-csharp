using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Proyecto.Api
{
    [Route("api/Pedidos/{action}", Name = "Pedidos")]
    public class PedidosController : ApiController
    {
        // Instancia del repositorio de pedidos
        IRepositorioPedidos pedidos = new RepositorioPedidos();

        #region Crud y listados
        /// <summary>
        /// Obtiene una lista de pedidos para un usuario específico.
        /// </summary>
        /// <param name="idUsuario">ID del usuario.</param>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de pedidos por usuario.</returns>
        [HttpGet]
        public HttpResponseMessage Listarporusuario(int idUsuario, DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(pedidos.ObtenerPedidosPorUsuario(idUsuario), loadOptions)
            );
        }

        /// <summary>
        /// Obtiene una lista de todos los pedidos.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de todos los pedidos.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(pedidos.obtenerPedidos(), loadOptions)
            );
        }

        /// <summary>
        /// Actualiza un pedido existente.
        /// </summary>
        /// <param name="form">Datos del formulario con el pedido a actualizar.</param>
        /// <returns>Respuesta HTTP con el resultado de la actualización.</returns>
        [HttpPut]
        public HttpResponseMessage Actualizar(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var pedido = pedidos.ObtenerPedidoPorId(key);

            JsonConvert.PopulateObject(values, pedido);

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Puedes formatear los errores como desees
                string errorMessage = "Error de validación: " + string.Join(", ", modelErrors);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

            pedidos.ActualizarPedido(pedido);
            return Request.CreateResponse(HttpStatusCode.OK, pedido);
        }
    }
    #endregion
}
