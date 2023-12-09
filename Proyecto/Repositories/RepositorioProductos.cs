using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioProductos : IRepositorioProductos
    {
        private TiendaEntities db = new TiendaEntities();
        public void ActualizarProducto(Producto producto)
        {
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AgregarProducto(Producto producto)
        {
            db.Producto.Add(producto);
            db.SaveChanges();
        }

        public void EliminarProducto(int idProducto)
        {
            var Producto = db.Producto.Find(idProducto);
            if (Producto != null)
            {
                db.Producto.Remove(Producto);
                db.SaveChanges();
            }
        }

        public Producto ObtenerProductoPorId(int idProducto)
        {
            return db.Producto.FirstOrDefault(r => r.IDProducto == idProducto);
        }

        public IEnumerable<Producto> ObtenerProductoPorCategoria(int categoria)
        {
            return db.Producto.Where(r => r.IDCategoria == categoria);
        }

        public IEnumerable<Producto> ObtenerTodosLosProductos()
        {
            return db.Producto.ToList();
        }
    }
}