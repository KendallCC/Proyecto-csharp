using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioDetallesPedido : IRepositorioDetallesPedido
    {
        TiendaEntities db = new TiendaEntities();
        public void AgregarDetallePedido(DetallePedido detallePedido)
        {
            db.DetallePedido.Add(detallePedido);
            db.SaveChanges();
        }

        public DetallePedido ObtenerDetallePorId(int idDetallePedido)
        {
            return db.DetallePedido.Find(idDetallePedido);
        }

        public IEnumerable<DetallePedido> ObtenerDetallesPorPedido(int idPedido)
        {
            return db.DetallePedido.Where(p => p.IDPedido == idPedido);
        }
    }
}