﻿using CapaEntidad;
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
     


        //index login
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();

            // Convertir el correo a mini
            string correoLower = correo.ToLower();

            // Buscar el usuario ignorando maryus/mini en el correo
            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo.ToLower() == correoLower && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {// envia a la misma vista que es index
                ViewBag.Error = "Correo o contraseña no correcta";
                return View();
            }
            else
            {
                if (oUsuario.Reestablecer)
                {
                    // guardar informacion y compartir de multiples vistas que estan dentro del mismo contralador
                    TempData["IdUsuario"] = oUsuario.IDUsuario;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);

                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }
        }



        //rrestablecer contrañsea usuario
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {

            Usuario ousurio = new Usuario();

            ousurio = new CN_Usuarios().Listar().Where(item => item.Correo == correo).FirstOrDefault();

            if (ousurio == null)
            {


                ViewBag.Error = "No se encontro un usuario relacionado a ese correo";
                return View();
            }


            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().ReestablecerClave(ousurio.IDUsuario, correo, out mensaje);

            if (respuesta)
            {

                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");

            }
            else
            {

                ViewBag.Error = mensaje;
                return View();
            }



        }






        //cambiar clave usuario el admin

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {

            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.IDUsuario == int.Parse(idusuario)).FirstOrDefault();

            if (oUsuario.Clave != CN_Recursos.ConvertirSha256(claveactual))
            {
                TempData["IdUsuario"] = idusuario;
                //almacenar valores como cadena de texto
                ViewData["vclave"] = "";   // confimar si la clave no es correcta
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {

                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = claveactual; //confirmacas la claves 
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";


            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);


            string mensaje = string.Empty;

            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaclave, out mensaje);

            if (respuesta)
            {

                return RedirectToAction("Index");
            }
            else
            {

                TempData["IdUsuario"] = idusuario;

                ViewBag.Error = mensaje;
                return View();
            }

        }
        
        //cerrar session
        public ActionResult CerrarSesion()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");

        }



    }
}
