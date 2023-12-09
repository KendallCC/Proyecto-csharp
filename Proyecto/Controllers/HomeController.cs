using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Repositories;
using Proyecto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region vista

        /// <summary>
        /// Acción para mostrar la página principal con productos filtrados por categoría.
        /// </summary>
        /// <param name="categoriaSeleccionada">ID de la categoría seleccionada (opcional).</param>
        /// <returns>Vista principal con productos filtrados.</returns>
        public ActionResult Index(int? categoriaSeleccionada)
        {
            try
            {
                // Repositorios
                IRepositorioProductos Productos = new RepositorioProductos();
            IRepositorioCategorias categorias = new RepositorioCategorias();

            // Obtener todas las categorías para mostrar en la vista
            ViewBag.categorias = categorias.ObtenerTodasLasCategorias();

            IEnumerable<Producto> productos;

            if (categoriaSeleccionada.HasValue)
            {
                // Obtener productos por categoría
                productos = Productos.ObtenerProductoPorCategoria(categoriaSeleccionada.Value);
                Categoria categoria = categorias.ObtenerCategoriaPorId((int)categoriaSeleccionada);
                ViewData["Descripcion"] = categoria.DescripcionCategoria;
            }
            else
            {
                // Obtener todos los productos si no se selecciona una categoría
                productos = Productos.ObtenerTodosLosProductos();
            }
                return View(productos);
            }
            catch (Exception ex)
            {
                // Log para registrar cualquier excepción ocurrida durante la adición al carrito
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Error", "Acceso");
            }
        }
        #endregion

        #region Acciones
        /// <summary>
        /// Acción para agregar un producto al carrito.
        /// </summary>
        /// <param name="productoId">ID del producto a agregar.</param>
        /// <param name="cantidad">Cantidad del producto a agregar.</param>
        /// <returns>Redirige a la página principal.</returns>
        public ActionResult AgregarAlCarrito(int productoId, int cantidad)
        {
            try
            {
                // Repositorios
                IRepositorioCarrito carrito = new RepositorioCarrito();
            IRepositorioProductos Productos = new RepositorioProductos();

            // Obtener el producto y el usuario actual
            var prod = Productos.ObtenerProductoPorId(productoId);
            var user = Session["usuario"] as Usuario;

            // Crear un objeto Carrito para agregar al carrito de compras
            Carrito compras = new Carrito();
            compras.Cantidad = cantidad;
            compras.IDUsuario = user.IDUsuario;
            compras.IDProducto = productoId;
            compras.Total = cantidad * prod.Precio;
            carrito.AgregarItemCarrito(compras);
                // Log para registrar la adición al carrito
                Log.Info($"El producto con ID {productoId} se agregó al carrito del usuario {user.IDUsuario}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log para registrar cualquier excepción ocurrida durante la adición al carrito
                Log.Error(ex,MethodBase.GetCurrentMethod());
                return RedirectToAction("Error", "Acceso");
            }
        }
        #endregion

        #region Metodo de vista para traduccion del DevExtreme
        /// <summary>
        /// Acción para proporcionar datos de localización para DevExtreme.
        /// </summary>
        /// <returns>Datos de localización para DevExtreme.</returns>
        public ActionResult CldrData()
        {
            return new DevExtreme.AspNet.Mvc.CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("es")
                .UseLocales("es", "es")
                .Build();
        }

        #endregion
    }
}
