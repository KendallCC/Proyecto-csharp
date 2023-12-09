using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Proyecto.Interfaces;
using Proyecto.Models;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Permisos;
using DevExtreme.AspNet.Data;
using Proyecto.Utilitarios;
using System.Reflection;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {

        #region Vista
        /// <summary>
        /// Obtiene la pagina de Productos
        /// </summary>
        /// <returns>ActionResult con la pagina para insercion de productos</returns>
        [PermisosRol(RolesPermisos.Empleados)]
        public ActionResult Index()
        {
            Log.Info("Ingresando a la vista de productos accesible por el empleado");
            return View();
        }
        #endregion

        #region Crud de producto
        /// <summary>
        /// Obtiene y carga los productos con opciones de filtrado y paginación.
        /// </summary>
        /// <param name="loadOptions">Opciones de carga.</param>
        /// <returns>Productos en formato JSON.</returns>
        [HttpGet]
        public object Listar(DataSourceLoadOptions loadOptions)
        {
            IRepositorioProductos product = new RepositorioProductos();
            List<Producto> lista = null;
            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = product.ObtenerTodosLosProductos().ToList();
                if (lista != null)
                {
                    var result = DataSourceLoader.Load(lista, loadOptions);
                    Log.Info("se filtra por categoria");
                    return Content(JsonConvert.SerializeObject(result), "application/json");
                }
                else
                {
                    Log.Info("No hay datos en la lista");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "NO HAY DATOS");
                }

            }
            catch (Exception ex)
            {
                // En caso de error, registrar la excepción y redirigir a una página de error
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Error", "Acceso");
            }
        }
        /// <summary>
        /// Inserta un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="values">Datos del producto a insertar.</param>
        /// <returns>Estado de la operación HTTP.</returns>
        [HttpPost]
        public ActionResult Insertar(string values)
        {
            IRepositorioProductos product = new RepositorioProductos();

            Producto producto = new Producto();
            try
            {
                string nombreFoto = Session["FotoRuta"] as string;


                if (nombreFoto!=null)
                {
                    producto.Imagen = "/Product-images/" + nombreFoto;
                }
                else
                {
                    producto.Imagen = "/Product-images/notfound.jpg";
                }

                Session["FotoRuta"] = null;

                JsonConvert.PopulateObject(values, producto);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

              
                
                product.AgregarProducto(producto);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
     

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un producto existente en la base de datos.
        /// </summary>
        /// <param name="key">Clave primaria del producto a actualizar.</param>
        /// <param name="values">Datos actualizados del producto.</param>
        /// <returns>Estado de la operación HTTP.</returns>
        [HttpPut]
        public ActionResult Actualizar(Int32 key, String values)
        {
            IRepositorioProductos product = new RepositorioProductos();
            Producto producto = new Producto();

            try
            {
                string nombreFoto = Session["FotoRuta"] as string;

                // Buscar por Id
                producto = product.ObtenerProductoPorId(Convert.ToInt32(key));
                // Si no existe
                if (producto == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe el producto No. {key}");
                }
                else
                {
                    // Si existe poblar oBodega con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, producto);
                }



                if (nombreFoto != null)
                {
                    producto.Imagen = "/Product-images/" + nombreFoto;
                    Session["FotoRuta"] = null;
                }
                

                // Validar el model
                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }



                product.ActualizarProducto(producto);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Elimina un producto existente en la base de datos.
        /// </summary>
        /// <param name="key">Clave primaria del producto a eliminar.</param>
        /// <returns>Estado de la operación HTTP.</returns>
        [HttpDelete]
        public ActionResult Borrar(Int32 key)
        {
            IRepositorioProductos product = new RepositorioProductos();
            try
            {

                string rutaCarpetaProyecto = Server.MapPath("~"); // Ruta de la carpeta en el proyecto

                var producto = product.ObtenerProductoPorId(key);
                string rutacompleta = "";
                if (producto.Imagen!= "notfound")
                {
                   rutacompleta = rutaCarpetaProyecto + producto.Imagen;
                }


                if (System.IO.File.Exists(rutacompleta))
                {
                    System.IO.File.Delete(rutacompleta);
                }

                product.EliminarProducto(Convert.ToInt32(key));

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        #endregion

        #region Subir archivos
        /// <summary>
        /// Sube un archivo al servidor y guarda su ruta en la sesión.
        /// </summary>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            var myFile = Request.Files["Foto"];
            var targetLocation = Server.MapPath("~/Product-images/");
            var uniqueFileName = string.Format("{0}_{1}{2}",
                Path.GetFileNameWithoutExtension(myFile.FileName),
                Request["key"],
                Path.GetExtension(myFile.FileName));
            try
            {
                var path = Path.Combine(targetLocation, uniqueFileName);
                myFile.SaveAs(path);
                Session["FotoRuta"] = uniqueFileName;
            }
            catch
            {
                Response.StatusCode = 400;
            }
            return new EmptyResult();
        }
        #endregion

        #region Metodo de vista para traduccion del DevExtreme
        /// <summary>
        /// Obtiene los datos CLDR para internacionalización en la interfaz de usuario.
        /// </summary>
        /// <returns>Script con los datos CLDR.</returns>
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