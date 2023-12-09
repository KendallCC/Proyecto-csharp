using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioPedidos
    {
        Pedido ObtenerPedidoPorId(int idPedido);
        IEnumerable<Pedido> ObtenerPedidosPorUsuario(int idUsuario);
        int RealizarPedido(Pedido pedido);
        void ActualizarPedido(Pedido pedido);
        IEnumerable<Pedido> obtenerPedidos();
    }
}
