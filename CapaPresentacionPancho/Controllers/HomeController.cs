using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;

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


        // Exportaventas ---> excel


        [HttpPost]
        public FileResult ExportarVenta(string fechainicio, string fechafin, string idtransaccion)
        {

            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Ventas(fechainicio, fechafin, idtransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("IdTransaccion", typeof(string));

            // foreach recorrer cada elemento
            foreach (Reporte rp in oLista)
            {
                dt.Rows.Add(new object[] {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Total,
                    rp.IdTransaccion
                });

            }

            dt.TableName = "Datos";

            //variable XLWorkbook para excel del nuget que hemos instalado ( ClosedXml )
            using (XLWorkbook xd = new XLWorkbook())
            {
                // hoja para el docuemnto
                xd.Worksheets.Add(dt);
                // variable MemoryStream
                using (MemoryStream stream = new MemoryStream())
                {
                    xd.SaveAs(stream);
                    //Configuracion para el excel
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVentaPancho" + DateTime.Now.ToString() + ".xlsx");

                }
            }





            // fin de corchetes//
        }
    }
}
