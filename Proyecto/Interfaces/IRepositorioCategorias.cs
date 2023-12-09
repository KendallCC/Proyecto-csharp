using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioCategorias
    {
        Categoria ObtenerCategoriaPorId(int idCategoria);
        IEnumerable<Categoria> ObtenerTodasLasCategorias();
        void AgregarCategoria(Categoria categoria);
        void ActualizarCategoria(Categoria categoria);
        void EliminarCategoria(int idCategoria);
    }
}
