using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioCarrito : IRepositorioCarrito
    {
        TiendaEntities db = new TiendaEntities();
        public void ActualizarItemCarrito(Carrito itemCarrito)
        {
            db.Entry(itemCarrito).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AgregarItemCarrito(Carrito itemCarrito)
        {
            db.Carrito.Add(itemCarrito);
            db.SaveChanges();
        }

        public void EliminarItemCarrito(int idItemCarrito)
        {
            var item = db.Carrito.Find(idItemCarrito);
            if (item != null)
            {
                db.Carrito.Remove(item);
                db.SaveChanges();
            }
        }

        public void EliminarCarritoUsuario(int idUsuario)
        {
            var item = ObtenerItemsCarritoPorUsuario(idUsuario);

            if (item.Count() > 0)
            {
                db.Carrito.RemoveRange(item);
                db.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }


        public Carrito ObtenerItemCarritoPorId(int idItemCarrito)
        {
            return db.Carrito.FirstOrDefault(r => r.IDCarrito == idItemCarrito);
        }

        public IEnumerable<Carrito> ObtenerItemsCarritoPorUsuario(int idUsuario)
        {
            return db.Carrito.Where(c=>c.IDUsuario==idUsuario);
        }
    }
}