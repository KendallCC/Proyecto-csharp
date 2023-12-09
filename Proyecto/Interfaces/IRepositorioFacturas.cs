using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Interfaces
{
    interface IRepositorioFacturas
    {
        Factura ObtenerFacturaPorId(int idFactura);
        IEnumerable<Factura> ObtenerFacturasPorPedido(int idPedido);
        void GenerarFactura(Factura factura);
        IEnumerable<Factura> ObtenerFacturas();
    }
}
