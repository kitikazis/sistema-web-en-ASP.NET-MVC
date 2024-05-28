using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IDProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Marca oMarca { get; set; }
        public Categoria oCategoria{ get; set; }

        //Precio
        public decimal Precio { get; set; }

        //Precio texto
        
        public string PrecioTexto { get; set; } 

        public int Stock { get; set; }
        public string  RutaImagen { get; set; }
        public string  NombreImagen { get; set; }
        public bool Activo { get; set; }
     //base 64 imagen
     public string Base64 { get; set; }
        // puede ser imagen png o jpg
        public string Extension { get; set; }

    }
}
