using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioHistorialPedidos
    {
        HistorialPedido ObtenerHistorialPedidoPorId(int idHistorialPedido);
        IEnumerable<HistorialPedido> ObtenerHistorialPedidosPorUsuario(int idUsuario);
        void AgregarHistorialPedido(HistorialPedido historialPedido);
    }
}
