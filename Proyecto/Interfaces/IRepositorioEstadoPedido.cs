using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioEstadoPedido
    {
        EstadoPedido ObtenerEstadoPorId(int idEstado);
        IEnumerable<EstadoPedido> ObtenerTodosLosEstadosPedido();
        void AgregarEstadoPedido(EstadoPedido estado);
        void ActualizarEstadoPedido(EstadoPedido estado);
        void EliminarEstadoPedido(int idEstado);
    }
}
