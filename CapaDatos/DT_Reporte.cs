using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class DT_Reporte
    {
        public List<ReporteVenta> RetornarVentas()
        {
            List<ReporteVenta> objLista = new List<ReporteVenta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
            {
                string query = "SP_RetornarVentas";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteVenta()
                        {
                            mes = dr["mes"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }

            return objLista;
        }

        public List<ReporteProducto> RetornarProductos()
        {
            List<ReporteProducto> objLista = new List<ReporteProducto>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
            {
                string query = "SP_RetornarProductos";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objLista.Add(new ReporteProducto()
                        {
                            producto = dr["producto"].ToString(),
                            cantidad = int.Parse(dr["cantidad"].ToString()),
                        });
                    }
                }
            }

            return objLista;
        }
    }
}
