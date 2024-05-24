
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> Listar() { 
            return objCapaDato.Listar();
        }


        public int Registrar(Usuario obj, out string Mensaje) {

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

                /////////
                ///




                string clave = "test123";
                obj.Clave = CN_Recursos.ConvertirSha256 (clave);

                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }

        }


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


        public bool Eliminar(int id, out string Mensaje)
        {
return objCapaDato.Eliminar(id,out Mensaje);

        }



    }
}
