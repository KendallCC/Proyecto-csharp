using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private TiendaEntities db = new TiendaEntities();
        public void ActualizarCategoria(Categoria categoria)
        {
            db.Entry(categoria).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AgregarCategoria(Categoria categoria)
        {
            db.Categoria.Add(categoria);
            db.SaveChanges();
        }

        public void EliminarCategoria(int idCategoria)
        {
            var Usuario = db.Categoria.Find(idCategoria);
            if (Usuario != null)
            {
                db.Categoria.Remove(Usuario);
                db.SaveChanges();
            }
        }

        public Categoria ObtenerCategoriaPorId(int idCategoria)
        {
            return db.Categoria.FirstOrDefault(r => r.IDCategoria == idCategoria);
        }

        public IEnumerable<Categoria> ObtenerTodasLasCategorias()
        {
            return db.Categoria.ToList();
        }
    }
}