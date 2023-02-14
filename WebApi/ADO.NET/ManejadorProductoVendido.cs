using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalWebAPI
{
    public class ManejadorProductoVendido
    {
        public static string cadenaConexion = "Data Source=DESKTOP-7QBII56\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True";

        // OBTENER LISTA PRODUCTO VENDIDOS
        public static List<Producto> ObtenerProductosVendidos(long idUsuario)
        {
            List<long> idProductos = new List<long>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando2 = new SqlCommand("SELECT IdProducto FROM Venta" +
                    " INNER JOIN ProductoVendido" +
                    " ON Venta.Id = ProductoVendido.IdVenta" +
                    " WHERE IdUsuario = @idUsuario", conn);
                comando2.Parameters.AddWithValue("@idUsuario", idUsuario);
                conn.Open();
                SqlDataReader reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idProductos.Add(reader.GetInt64(0));
                    }
                }
            }
            List<Producto> productos = new List<Producto>();
            foreach (var id in idProductos)
            {
                Producto prodTemp = ManejadorProducto.ObtenerProducto(id);
                productos.Add(prodTemp);
            }
            return productos;
        }
    }
}
