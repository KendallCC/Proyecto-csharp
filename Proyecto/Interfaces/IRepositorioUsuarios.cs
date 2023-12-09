using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioUsuarios
    {
        Usuario ObtenerUsuarioPorId(int idUsuario);
        IEnumerable<Usuario> ObtenerTodosLosUsuarios();
        void AgregarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int idUsuario);
        Usuario AutenticarUsuario(string contra,string correo);
        bool ObtenerUsuarioPorcorreo(string correo);
        RolUsuario ObtenerRolPorUsuario(int idusuario);

    }
}
