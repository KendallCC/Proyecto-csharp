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
    [Route("api/EstadoPedido/{action}", Name = "EstadoPedido")]
    public class EstadoPedidoController : ApiController
    {

        // Instancia del repositorio de EstadoPedido
        IRepositorioEstadoPedido repositorio = new RepositorioEstadosPedido();

        #region Listar Estados Pedido
        /// <summary>
        /// Obtiene una lista de estados de pedido.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de estados de pedido.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(repositorio.ObtenerTodosLosEstadosPedido(), loadOptions)
            );
        }
        #endregion

    }
}
