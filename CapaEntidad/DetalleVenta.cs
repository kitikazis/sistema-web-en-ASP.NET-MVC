using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int IDDetalleVenta { get; set; }
        public int IDVenta { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }   
        public decimal Total { get; set; }
        public string IDTransaccion { get; set; }
    }
}