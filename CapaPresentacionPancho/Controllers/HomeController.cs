using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionPancho.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }
        //listar usuarios
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new CN_Usuarios().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            if (objeto.IDUsuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        //Eliminar Usuario
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            //punto depuracion 52
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);



        }


        //Vista Dasrboard - Contadores


        [HttpGet]
        public JsonResult VistaDashBoard()
        {
            DashBoard objeto = new CN_Reporte().VerDashBoard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }
    

        // Lista Reporte
        [HttpGet]
        public JsonResult ListaReporte(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        // 





    }
}
