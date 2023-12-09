using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioRoles
    {
        RolUsuario ObtenerRolPorId(int idRol);
        IEnumerable<RolUsuario> ObtenerTodosLosRoles();
        void AgregarRol(RolUsuario rol);
        void ActualizarRol(RolUsuario rol);
        void EliminarRol(int idRol);
    }
}
