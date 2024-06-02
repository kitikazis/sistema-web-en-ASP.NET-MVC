
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;
using System.Diagnostics.Eventing.Reader;
namespace CapaNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();
        // Listar Usuario
        public List<Usuario> Listar()
        {
            return objCapaDato.Listar();
        }

        //registra Usuario
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "EL nombre del usuario no puede ser vacio";


            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "EL Apellidos del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "EL Correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                //Generar claves = CN RECURSOS 
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3><br><p>Su contraseña para acceder a su cuenta es: !clave!</p>";

                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);
                if (respuesta)
                {

                    //actulizar y encriptart la clave registrada mano
                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }

            }
            else
            {


                return 0;
            }
        }

        //Editar
        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "EL nombre del usuario no puede ser vacio";


            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "EL Apellidos del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "EL Correo del usuario no puede ser vacio";
            }
            if(string.IsNullOrEmpty(Mensaje)) {
                return objCapaDato.Editar(obj, out Mensaje);    
        }
else
            {

            return false; } }

        // Eliminar 
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id,out Mensaje);

        }

        //cmaibar clave usuarios
        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {

            return objCapaDato.CambiarClave(idusuario, nuevaclave, out Mensaje);
        }

        //reees usuarios
        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {

            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            //encriptacion recurso sha256
            bool resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);

            if (resultado)
            {

                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);


                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }

            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";

                return false;
            }


        }








    }
}

