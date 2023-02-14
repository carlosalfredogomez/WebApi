using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class ManejadorVentas
    {
        // Cadena de Conexión
        public static string cadenaConexion = "Data Source=QT5\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // OBTENER VENTA POR ID
        public static Venta ObtenerVenta(long id)
        {
            Venta venta = new Venta();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Venta WHERE Id = @id", conexion);
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    venta.Id = reader.GetInt64(0);
                    venta.Comentarios = reader.GetString(1);
                    venta.IdUsuario = reader.GetInt64(2);
                }
            }
            return venta;
        }

        // OBTENER LISTA VENTAS RECIBE ID USUARIO
        public static List<Venta> ObtenerVentas(long idUsuario)
        {
            List<long> ventaRealizada = new List<long>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Id FROM Venta " +
                    " WHERE IdUsuario = @idUsuario", conexion);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ventaRealizada.Add(reader.GetInt64(0));
                    }
                }
            }
            List<Venta> listaVentasRealizadas = new List<Venta>();
            foreach (var id in ventaRealizada)
            {
                Venta ventaTempporal = ObtenerVenta(id);
                listaVentasRealizadas.Add(ventaTempporal);
            }
            return listaVentasRealizadas;
        }

        // INSERTAR VENTA
        public static void InsertarVenta(List<Producto> productos, long IdUsuario)
        {
            Venta venta = new Venta();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Venta (Comentarios, IdUsuario) " + 
                    " VALUES (Comentarios = @comentarios, IdUsuario = @idUsuario", conexion);
                comando.Parameters.AddWithValue("@comentarios", venta.Comentarios);
                comando.Parameters.AddWithValue("@idUsuario", venta.IdUsuario);
                conexion.Open();
                comando.ExecuteNonQuery();
                venta.Id = GetId.Get(comando);
                foreach (Producto producto in productos)
                {
                    SqlCommand comando2 = new SqlCommand("INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) " +
                        " VALUES (Stock = @stock, IpProducto = @idProducto, IdVenta = @idVenta", conexion);
                    comando2.Parameters.AddWithValue("@stock", producto.Stock);
                    comando2.Parameters.AddWithValue("@idProducto", producto.Id);
                    comando2.Parameters.AddWithValue("@idVenta", venta.Id);
                    comando2.ExecuteNonQuery();
                    
                    SqlCommand comando3 = new SqlCommand("UPDATE Producto SET Stock = (Stock - @stock) " + 
                        " WHERE Id = @idProducto", conexion);
                    comando3.Parameters.AddWithValue("@stock", producto.Stock);
                    comando3.Parameters.AddWithValue("@idProducto", producto.Id);
                    comando3.ExecuteNonQuery();
                }
            }
        }
    }
}

