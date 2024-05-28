using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//Correo
using System.Net.Mail;
using System.Net;
using System.IO;

namespace CapaNegocio
{




    public class CN_Recursos
    {

        //GENERAR CLAVES 
        public static string GenerarClave()
        {
            //Retornar - Formato N(alfanumericos) - Limite de cantidad
            string clave = Guid.NewGuid().ToString("N").Substring(0, 9);
            return clave;
        }
        //Encriptar contraseñas SHA256
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));


            }
            return Sb.ToString();

        }
        //ENVIAR CORREO
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("luiskitikazis2@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                //servidor para el envio de correo
                var smtp = new SmtpClient()
                {
                    //contrasñe de aplicaciones
                    Credentials = new NetworkCredential("luiskitikazis2@gmail.com", "qyprvvfqlvxcktzj"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                //metodo send
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;

            }
            return resultado;

        }


        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] btyes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(btyes);

            }
            catch
            {
                conversion = false;

            }
            return textoBase64;

        }
    }
}
