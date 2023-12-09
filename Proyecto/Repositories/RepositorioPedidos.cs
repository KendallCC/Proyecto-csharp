using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioPedidos : IRepositorioPedidos
    {
        private TiendaEntities db = new TiendaEntities();
        public void ActualizarPedido(Pedido pedido)
        {
            db.Entry(pedido).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Pedido ObtenerPedidoPorId(int idPedido)
        {
            return db.Pedido.Find(idPedido);
        }

        public IEnumerable<Pedido> ObtenerPedidosPorUsuario(int idUsuario)
        {
            return db.Pedido.Where(r => r.IDUsuario == idUsuario);
        }

        public int RealizarPedido(Pedido pedido)
        {
            db.Pedido.Add(pedido);
            db.SaveChanges();
            return pedido.IDPedido;
        }

        public IEnumerable<Pedido> obtenerPedidos()
        {
            return db.Pedido.ToList();
        }

    }
}