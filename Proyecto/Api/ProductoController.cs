using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Proyecto.Interfaces;
using Proyecto.Repositories;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Proyecto.Api
{
    [Route("api/Producto/{action}", Name = "Producto")]
    public class ProductoController : ApiController
    {
        // Instancia del repositorio de productos
        IRepositorioProductos Producto = new RepositorioProductos();

        #region Listado de productos
        /// <summary>
        /// Obtiene una lista de todos los productos.
        /// </summary>
        /// <param name="loadOptions">Opciones para cargar y paginar los datos.</param>
        /// <returns>Lista de todos los productos.</returns>
        [HttpGet]
        public HttpResponseMessage Listar(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(
                DataSourceLoader.Load(Producto.ObtenerTodosLosProductos(), loadOptions)
            );
        }
        #endregion

    }
}
