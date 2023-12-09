using System;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Utilitarios
{
    public class EnviarCorreo
    {
        public String CuentaCorreoElectronico = "utnpruebaisw711@gmail.com";
        public String ContrasenaGeneradaGmail = "fhplwjoogyjbpftd";

        public EnviarCorreo()
        {
        }

        public void enviarCorreoGmail(string body, string receptor, string asunto, byte[] adjuntoBytes, string nombreAdjunto)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.IsBodyHtml = true;
            mensaje.Subject = asunto;
            mensaje.Body = body;
            mensaje.IsBodyHtml = true;
            mensaje.From = new MailAddress(CuentaCorreoElectronico);
            mensaje.To.Add(receptor);  // Correo del destinatario

            // Adjuntar el archivo PDF
            if (adjuntoBytes != null && adjuntoBytes.Length > 0)
            {
                MemoryStream ms = new MemoryStream(adjuntoBytes);
                mensaje.Attachments.Add(new Attachment(ms, nombreAdjunto, "application/pdf"));
            }

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(CuentaCorreoElectronico, ContrasenaGeneradaGmail);
            smtp.EnableSsl = true;

            smtp.Send(mensaje);
        }


    }
}
