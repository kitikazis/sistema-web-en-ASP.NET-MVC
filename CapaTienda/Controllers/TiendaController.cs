using CapaEntidad;

using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CapaTienda.Filter;
using CapaEntidad.Paypal;

namespace CapaTienda.Controllers
{

    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DetalleProducto(int idproducto = 0)
        {

            Producto oProducto = new Producto();
            bool conversion;


            oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();


            if (oProducto != null)
            {
                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.NombreImagen);
            }

            return View(oProducto);
        }



        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMarcaporCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();
            lista = new CN_Marca().ListarMarcaporCategoria(idcategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarProducto(int idcategoria, int idmarca, int nroPagina)
        {
            List<Producto> lista = new List<Producto>();

            bool conversion;

            //lista = new CN_Producto().Listar().Select(p => new Producto()
            //{
            //    IdProducto = p.IdProducto,
            //    Nombre = p.Nombre,
            //    Descripcion = p.Descripcion,
            //    oMarca = p.oMarca,
            //    oCategoria = p.oCategoria,
            //    Precio = p.Precio,
            //    Stock = p.Stock,
            //    RutaImagen = p.RutaImagen,
            //    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
            //    Extension = Path.GetExtension(p.NombreImagen),
            //    Activo = p.Activo
            //}).Where(p =>
            //    p.oCategoria.IdCategoria == (idcategoria == 0 ? p.oCategoria.IdCategoria : idcategoria) &&
            //    p.oMarca.IdMarca == (idmarca == 0 ? p.oMarca.IdMarca : idmarca) &&
            //    p.Stock > 0 && p.Activo == true
            //).ToList();

            int _TotalRegistros;
            int _TotalPaginas;

            lista = new CN_Producto().ObtenerProductos(idmarca, idcategoria, nroPagina, 8, out _TotalRegistros, out _TotalPaginas).Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oMarca = p.oMarca,
                oCategoria = p.oCategoria,
                Precio = p.Precio,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Activo = p.Activo
            }).ToList();


            var jsonresult = Json(new { data = lista, totalRegistros = _TotalRegistros, totalPaginas = _TotalPaginas }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;


        }

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            bool existe = new CN_Carrito().ExisteCarrito(idcliente, idproducto);

            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";

            }
            else
            {

                respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public JsonResult CantidadEnCarrito()
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idcliente);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ListarProductosCarrito()
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<Carrito> oLista = new List<Carrito>();

            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                oProducto = new Producto()
                {
                    IdProducto = oc.oProducto.IdProducto,
                    Nombre = oc.oProducto.Nombre,
                    oMarca = oc.oProducto.oMarca,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)

                },
                Cantidad = oc.Cantidad
            }).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;


            bool respuesta = false;

            string mensaje = string.Empty;


            respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);


            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idproducto)
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            bool respuesta = false;

            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcliente, idproducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerDepartamento()
        {

            List<Departamento> oLista = new List<Departamento>();

            oLista = new CN_Ubicacion().ObtenerDepartamento();

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerProvincia(string IdDepartamento)
        {

            List<Provincia> oLista = new List<Provincia>();

            oLista = new CN_Ubicacion().ObtenerProvincia(IdDepartamento);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ObtenerDistrito(string IdDepartamento, string IdProvincia)
        {

            List<Distrito> oLista = new List<Distrito>();

            oLista = new CN_Ubicacion().ObtenerDistrito(IdDepartamento, IdProvincia);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        public ActionResult Carrito()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> ProcesarPago(List<Carrito> oListaCarrito, Venta oVenta)
        {

            decimal total = 0;
            DataTable detalle_venta = new DataTable();
            detalle_venta.Locale = new CultureInfo("es-PE");
            detalle_venta.Columns.Add("IdProducto", typeof(string));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Total", typeof(decimal));


            List<Item> oListaItem = new List<Item>();

            foreach (Carrito oCarrito in oListaCarrito)
            {
                decimal subtotal = Convert.ToDecimal(oCarrito.Cantidad.ToString()) * oCarrito.oProducto.Precio;

                total += subtotal;

                oListaItem.Add(new Item()
                {
                    name = oCarrito.oProducto.Nombre,
                    quantity = oCarrito.Cantidad.ToString(),
                    unit_amount = new UnitAmount()
                    {
                        currency_code = "USD",
                        value = oCarrito.oProducto.Precio.ToString("G", new CultureInfo("es-PE"))
                    }


                });

                detalle_venta.Rows.Add(new object[] {
                    oCarrito.oProducto.IdProducto,
                    oCarrito.Cantidad,
                    subtotal
                });
            }


            PurchaseUnit purchaseUnit = new PurchaseUnit()
            {
                amount = new Amount()
                {
                    currency_code = "USD",
                    value = total.ToString("G", new CultureInfo("es-PE")),
                    breakdown = new Breakdown()
                    {
                        item_total = new ItemTotal()
                        {
                            currency_code = "USD",
                            value = total.ToString("G", new CultureInfo("es-PE")),
                        }
                    }

                },
                description = "compra de articulo de mi tienda",
                items = oListaItem

            };


            Checkout_Order oCheckOutOrder = new Checkout_Order()
            {
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>() { purchaseUnit },
                application_context = new ApplicationContext()
                {
                    brand_name = "MiTienda.com",
                    landing_page = "NO_PREFERENCE",
                    user_action = "PAY_NOW",
                    //confiruado segun la url local o nube
                    return_url = "https://localhost:44323/Tienda/PagoEfectuado",
                    cancel_url = "https://localhost:44323/Tienda/Carrito"

                }

            };




            oVenta.MontoTotal = total;
            oVenta.IdCliente = ((Cliente)Session["Cliente"]).IdCliente;

            TempData["Venta"] = oVenta;
            TempData["DetalleVenta"] = detalle_venta;


            CN_Paypal opaypal = new CN_Paypal();

            Response_Paypal<Response_Checkout> response_paypal = new Response_Paypal<Response_Checkout>();

            response_paypal = await opaypal.CrearSolicitud(oCheckOutOrder);




            return Json(response_paypal, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession]
        [Authorize]
        public async Task<ActionResult> PagoEfectuado()
        {

            string token = Request.QueryString["token"];

            CN_Paypal opaypal = new CN_Paypal();
            Response_Paypal<Response_Capture> response_paypal = new Response_Paypal<Response_Capture>();
            response_paypal = await opaypal.AprobarPago(token);


            ViewData["Status"] = response_paypal.Status;

            if (response_paypal.Status)
            {

                Venta oVenta = (Venta)TempData["Venta"];

                DataTable detalle_venta = (DataTable)TempData["DetalleVenta"];

                oVenta.IdTransaccion = response_paypal.Response.purchase_units[0].payments.captures[0].id;

                string mensaje = string.Empty;

                bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);


                ViewData["IdTransaccion"] = oVenta.IdTransaccion;

            }

            return View();

        }

        [ValidarSession]
        [Authorize]
        public ActionResult MisCompras()
        {

            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<DetalleVenta> oLista = new List<DetalleVenta>();

            bool conversion;

            oLista = new CN_Venta().ListarCompras(idcliente).Select(oc => new DetalleVenta()
            {
                oProducto = new Producto()
                {
                    Nombre = oc.oProducto.Nombre,
                    Precio = oc.oProducto.Precio,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)

                },
                Cantidad = oc.Cantidad,
                Total = oc.Total,
                IdTransaccion = oc.IdTransaccion
            }).ToList();

            return View(oLista);

        }



    }
}