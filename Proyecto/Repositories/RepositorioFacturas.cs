using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioFacturas : IRepositorioFacturas
    {
        TiendaEntities db = new TiendaEntities();
        public void GenerarFactura(Factura factura)
        {
            db.Factura.Add(factura);
            db.SaveChanges();
        }

        public Factura ObtenerFacturaPorId(int idFactura)
        {
            return db.Factura.FirstOrDefault(r => r.IDFactura == idFactura);
        }

        public IEnumerable<Factura> ObtenerFacturasPorPedido(int idPedido)
        {
            return db.Factura.Where(e => e.IDPedido == idPedido);
        }

        public IEnumerable<Factura> ObtenerFacturas()
        {
            return db.Factura.ToList();
        }
    }
}