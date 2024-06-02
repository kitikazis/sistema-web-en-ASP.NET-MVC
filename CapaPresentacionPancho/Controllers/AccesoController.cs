using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionPancho.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
            public ActionResult CambiarClave()
            {
                return View();
            }
            public ActionResult Reestablecer()
            {
                return View();
            }



        //index login
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        { // punto de depuracion
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();


            if (oUsuario == null)
            {

                ViewBag.Error = "Correo o contraseña no correcta";
                return View();
            }
            else
            {

                

                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}