using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioRoles : IRepositorioRoles
    {
        private TiendaEntities db = new TiendaEntities();
        public void ActualizarRol(RolUsuario rol)
        {
            db.Entry(rol).State=EntityState.Modified;
            db.SaveChanges();
        }

        public void AgregarRol(RolUsuario rol)
        {
            db.RolUsuario.Add(rol);
            db.SaveChanges();
        }

        public void EliminarRol(int idRol)
        {
            var rol = db.RolUsuario.Find(idRol);
            if (rol != null)
            {
                db.RolUsuario.Remove(rol);
                db.SaveChanges();
            }
        }

        public RolUsuario ObtenerRolPorId(int idRol)
        {
          return  db.RolUsuario.FirstOrDefault(r => r.RoleID == idRol);
        }

        

        public IEnumerable<RolUsuario> ObtenerTodosLosRoles()
        {
            return db.RolUsuario.ToList();
        }



    }
}