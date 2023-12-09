using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioDetallesPedido
    {
        DetallePedido ObtenerDetallePorId(int idDetallePedido);
        IEnumerable<DetallePedido> ObtenerDetallesPorPedido(int idPedido);
        void AgregarDetallePedido(DetallePedido detallePedido);
    }
}
