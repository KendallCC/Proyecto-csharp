using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Proyecto.Utilitarios
{
    public class ServiceBCCR
    {
        //Agregar los credenciales para el uso del consumo API del Dolar BCCR
        private readonly string TOKEN = "R9MOVE2OVE";
        private readonly string NOMBRE = "ROOSVELT";
        private readonly string CORREO = "roswel030@gmail.com";

        public double GetMontoVentaHoy()
        {
            // Obtener la fecha actual
            DateTime today = DateTime.Today;

            // Protocolo de comunicaciones
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            // Se instancia al Servicio Web
            BCCR.wsindicadoreseconomicosSoapClient client =
                new BCCR.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");

            // Se invoca.
            DataSet dataset = client.ObtenerIndicadoresEconomicos("318", today.ToString("dd/MM/yyyy"), today.ToString("dd/MM/yyyy"), NOMBRE, "N", CORREO, TOKEN);

            //Se carga el dataset
            DataTable table = dataset.Tables[0];

            //Se busca el monto de venta de hoy
            DataRow todayRow = table.Rows.Cast<DataRow>().FirstOrDefault(row => DateTime.Parse(row[1].ToString()).Date == today);

            if (todayRow != null)
            {
                //Se retorna el monto de venta de hoy
                return Convert.ToDouble(todayRow[2]);
            }

            //Si no se encontró información para hoy, se retorna 0
            return 0;
        }
    }

}