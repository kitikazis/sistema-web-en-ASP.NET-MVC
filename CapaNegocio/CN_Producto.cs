using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        //producto
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        //registra Marca
        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "EL nombre del Producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La Descripcion de la categoria no puede estar vacia";
            }
            else if (obj.oMarca.IDMarca == 0)
            {
                Mensaje = "Debe selecciona una marca";
            }
            else if (obj.oCategoria.IDCategoria == 0)
            {
            }
            else if (obj.oCategoria.IDCategoria == 0)
            {
                Mensaje = "Debe selecionar una Categoria";
            }
            else if (obj.oCategoria.IDCategoria == 0)
            {
                Mensaje = "Debe selecionar una Categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el stock del producto";

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
        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacío.";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción del producto no puede ser vacía.";
            }
            else if (obj.oMarca.IDMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca.";
            }
            else if (obj.oCategoria.IDCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoría.";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresar el precio del producto.";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresar el stock del producto.";
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

        //Guardar datos de Imagen
        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {

            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }


        //elimianr Marca
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);

        }
    }
    













}
