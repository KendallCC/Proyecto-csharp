using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Proyecto.Interfaces;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.Api
{
    [Route("api/DetallePedido/{action}", Name = "DetallePedido")]
    public class DetallePedidoController : ApiController
    {
        // Instancia del repositorio de Detalles de Pedido
        IRepositorioDetallesPedido pedidos = new RepositorioDetallesPedido();

        #region Listar Pedidos
        /// <summary>
        /// Obtiene una lista de detalles de pedido por ID de pedido.
        /// </summary>
        /// <param name="id">ID del pedido para obtener los detalles asociados.</param>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de detalles de pedido para el pedido especificado.</returns>
        [HttpGet]
        public HttpResponseMessage ListarporPedido(int id, DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(pedidos.ObtenerDetallesPorPedido(id), loadOptions)
            );
        }
        #endregion

    }
}
