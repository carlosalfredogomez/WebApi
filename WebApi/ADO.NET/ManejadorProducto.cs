using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalWebAPI
{
    public class ManejadorProducto
    {
        // Cadena de Conexión
        public static string cadenaConexion = "Data Source=DESKTOP-7QBII56\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True";

        // OBTENER PRODUCTO POR ID- OK
        public static Producto ObtenerProducto(long id)
        {
            Producto producto = new Producto();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando2 = new SqlCommand("SELECT * FROM Producto WHERE Id = @id", conn);
                comando2.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);
                }
            }
            return producto;
        }

        // OBTENER PRODUCTO POR DESCRIPCIONES
        public static Producto ObtenerProducto(string descripciones)
        {
            Producto producto = new Producto();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando2 = new SqlCommand("SELECT * FROM Producto WHERE Descripciones = @descripciones", conn);
                comando2.Parameters.AddWithValue("@descripciones", descripciones);
                conn.Open();
                SqlDataReader reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);
                }
            }
            return producto;
        }

        // INSERTAR PRODUCTO - OK
        public static int InsertarProducto(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario)" +
                    "VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conn);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }

        // ELIMINAR PRODUCTO - OK
        public static int DeleteProducto(long id)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand($"DELETE FROM ProductoVendido WHERE IdProducto = @id", conexion);
                    SqlCommand comando1 = new SqlCommand($"DELETE FROM Producto WHERE Id=@id", conexion);
                    conexion.Open();
                    comando.Parameters.AddWithValue("id", id);
                    comando1.Parameters.AddWithValue("id", id); 
                    comando.ExecuteNonQuery();
                    return comando1.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("" + e.Message);
                    return -1;
                }
            }
        }

        // ACTUALIZAR PRODUCTO - OK
        public static int UpdateProducto(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("UPDATE Producto " +
                    "SET Descripciones=@descripciones," +
                    "Costo=@costo, PrecioVenta=@precioVenta," +
                    "Stock=@stock, IdUsuario=@idUsuario " +
                    "WHERE Id=@id", conn);
                comando.Parameters.AddWithValue("@id", producto.Id);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }

        // OBTENER LISTA DE PRODUCTOS - OK  
        public static List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto", conn);
                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemporal = new Producto();
                        productoTemporal.Id = reader.GetInt64(0);
                        productoTemporal.Descripciones = reader.GetString(1);
                        productoTemporal.Costo = reader.GetDecimal(2);
                        productoTemporal.PrecioVenta = reader.GetDecimal(3);
                        productoTemporal.Stock = reader.GetInt32(4);
                        productoTemporal.IdUsuario = reader.GetInt64(5);
                        productos.Add(productoTemporal);
                    }
                }
                return productos;
            }
        }

        // OBTENER LISTA PRODUCTOS RECIBE ID USUARIO
        public static List<Producto> ObtenerProductosCargados(long idUsuario)
        {
            List<long> productoCargado = new List<long>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT Id FROM Producto " +
                    " WHERE IdUsuario = @idUsuario", conexion);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productoCargado.Add(reader.GetInt64(0));
                    }
                }
            }
            List<Producto> listaProductosCargados = new List<Producto>();
            foreach (var id in productoCargado)
            {
                Producto productoTempporal = ObtenerProducto(id);
                listaProductosCargados.Add(productoTempporal);
            }
            return listaProductosCargados;
        }

        // ACTUALIZAR STOCK PRODUCTO VENDIDO
        public static int UpdateStockProducto(long id, int cantidadVendidos)
        {
            Producto producto = ObtenerProducto(id);
            producto.Stock -= cantidadVendidos;
            return UpdateProducto(producto);
        }
    }
}
