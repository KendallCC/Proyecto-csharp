using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioCarrito
    {
        Carrito ObtenerItemCarritoPorId(int idItemCarrito);
        IEnumerable<Carrito> ObtenerItemsCarritoPorUsuario(int idUsuario);
        void AgregarItemCarrito(Carrito itemCarrito);
        void ActualizarItemCarrito(Carrito itemCarrito);
        void EliminarCarritoUsuario(int idUsuario);
        void EliminarItemCarrito(int idItemCarrito);
    }
}
