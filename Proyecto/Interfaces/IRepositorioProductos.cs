using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioProductos
    {
        Producto ObtenerProductoPorId(int idProducto);
        IEnumerable<Producto> ObtenerTodosLosProductos();
        void AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void EliminarProducto(int idProducto);
        IEnumerable<Producto> ObtenerProductoPorCategoria(int categoria);
    }
}
