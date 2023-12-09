using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioEstadosPedido : IRepositorioEstadoPedido
    {
        TiendaEntities db = new TiendaEntities();

        public void ActualizarEstadoPedido(EstadoPedido estado)
        {
            throw new NotImplementedException();
        }

        public void AgregarEstadoPedido(EstadoPedido estado)
        {
            throw new NotImplementedException();
        }

        public void EliminarEstadoPedido(int idEstado)
        {
            throw new NotImplementedException();
        }

        public EstadoPedido ObtenerEstadoPorId(int idEstado)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EstadoPedido> ObtenerTodosLosEstadosPedido()
        {
            return db.EstadoPedido.ToList();
        }
    }
}