using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marca
    {
        //Marca
        private CD_Marca objCapaDato = new CD_Marca();
        public List<Marca> Listar()
        {
            return objCapaDato.Listar();
        }

        //registra Marca
        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "EL nombre del Marca no puede ser vacio";


            }


            if (string.IsNullOrEmpty(Mensaje))
            {


                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {

                return 0;
            }

        }



        //Editar Marca
        public bool Editar(Marca obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcon de la Marca no puede ser vacia";


            }
            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {

                return false;
            }
        }



        //elimianr Marca
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);

        }




        //listar marca x categoria
        public List<Marca> ListarMarcaporCategoria(int idcategoria)
        {

            return objCapaDato.ListarMarcaporCategoria(idcategoria);
        }








    }
}
