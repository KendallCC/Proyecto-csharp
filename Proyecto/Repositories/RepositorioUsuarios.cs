using Proyecto.Interfaces;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proyecto.Repositories
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private TiendaEntities db = new TiendaEntities();
        public void ActualizarUsuario(Usuario usuario)
        {
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AgregarUsuario(Usuario usuario)
        {
            db.Usuario.Add(usuario);
            db.SaveChanges(); 
        }

        public Usuario AutenticarUsuario(string correo, string contra)
        {
            return db.Usuario.FirstOrDefault(u => u.CorreoElectronico == correo && u.Contraseña == contra);
        }


        public void EliminarUsuario(int idUsuario)
        {
            var Usuario = db.Usuario.Find(idUsuario);
            if (Usuario != null)
            {
                db.Usuario.Remove(Usuario);
                db.SaveChanges();
            }
        }

        public IEnumerable<Usuario> ObtenerTodosLosUsuarios()
        {
            return db.Usuario.ToList();
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            return db.Usuario.FirstOrDefault(r => r.IDUsuario == idUsuario);
        }

        public bool ObtenerUsuarioPorcorreo(string correo)
        {
            return db.Usuario.FirstOrDefault(r => r.CorreoElectronico == correo)!=null;
        }

        public RolUsuario ObtenerRolPorUsuario(int idusuario)
        {
            var usuario = db.Usuario.FirstOrDefault(u => u.IDUsuario == idusuario);

            if (usuario != null)
            {
                return usuario.RolUsuario;
            }

            return null; // Retorna null si el usuario no se encuentra o si no tiene un rol asociado.
        }

    }
}