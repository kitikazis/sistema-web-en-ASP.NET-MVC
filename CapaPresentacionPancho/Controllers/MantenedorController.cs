using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionPancho.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }

        // -- CATEGORIA-- //
        #region CATEGORIA
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        //xd
        //guardar categoria
        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IDCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        //Eliminar Categoria
        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            //punto depuracion 52
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);



        }

        #endregion



        // -- Marca -- //
        #region Marca
        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //guardar categoria
        [HttpPost]
        public JsonResult GuardaMarca(Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IDMarca == 0)
            {
                resultado = new CN_Marca().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        //Eliminar Categoria
        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            //punto depuracion 52
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);



        }

        #endregion


        // -- PRODCUTO -- //
        #region Producto
        //listar
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //Guardar Producto
        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {

            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out precio))
            {
                oProducto.Precio = precio;

            }
            else

            {
                return Json(new { operacionExtosa = false, mensaje = "El formtado del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }



            if (oProducto.IDProducto == 0)
            {
                int IDProductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);
                if (IDProductoGenerado != 0)
                {
                    oProducto.IDProducto = IDProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }
            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);

            }
            if (operacion_exitosa)
            {
                if (archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IDProducto.ToString(), extension);
                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }
                    if (!guardar_imagen_exito)
                    {
                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el prodcuto pero hubo problemas con la imagen";
                    }
                }
            }
            return Json(new { operacionExtosa = operacion_exitosa, IDGenerado = oProducto.IDProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto = new CN_Producto().Listar().Where(p => p.IDProducto == id).FirstOrDefault();
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
            return Json(new
            {
                conversion = conversion,
                textobase64 = textoBase64,
                extension = Path.GetExtension(oProducto.NombreImagen)
            },
            JsonRequestBehavior.AllowGet
       );

        }

        //Eliminar Producto
        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            //punto depuracion 52
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);



            #endregion




        }
    }
}