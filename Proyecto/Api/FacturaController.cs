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
    [Route("api/Factura/{action}", Name = "Factura")]
    public class FacturaController : ApiController
    {
        // Instancia del repositorio de Facturas
        IRepositorioFacturas Factura = new RepositorioFacturas();

        #region Listar Facturas
        /// <summary>
        /// Obtiene una lista de facturas.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de facturas.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(Factura.ObtenerFacturas(), loadOptions)
            );
        }
        #endregion

    }
}
