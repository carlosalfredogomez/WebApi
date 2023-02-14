using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalWebAPI
{
    public class ManejadorUsuario
    {
        // Cadena de Conexión
        public static string cadenaConexion = "Data Source=DESKTOP-7QBII56\\SQLEXPRESS;Initial Catalog=SistemaGestion;Integrated Security=True";

        // OBTENER USUARIO POR ID
        public static Usuario ObtenerUsuario(long id)
        {
            Usuario usuario = new Usuario();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE Id = @id", conexion);
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Password = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);
                }
            }
            return usuario;
        }

        // INSERTAR USUARIO - OK
        public static int InsertarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail)" +
                    "VALUES (@nombre, @apellido, @nombreUsuario, @password, @mail)", conn);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@password", usuario.Password);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);
                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }

        // ACTUALIZAR USUARIO - OK
        public static int UpdateUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("UPDATE Usuario " +
                    " SET Nombre = @nombre, Apellido = @apellido, " +
                    " NombreUsuario = @nombreUsuario, Contraseña = @password," +
                    " Mail = @mail WHERE Id = @id", conn);
                comando.Parameters.AddWithValue("@id", usuario.Id);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@password", usuario.Password);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);
                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }

        // LOGUIN USUARIO - OK
        public static Usuario Login(string nombreUsuario, string password)
        {
            Usuario usuarioMatch = new Usuario();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario " +
                    " WHERE NombreUsuario = @nombreUsuario and Contraseña = @password", conexion);
                comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@password", password);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuarioMatch.Id = reader.GetInt64(0);
                    usuarioMatch.Nombre = reader.GetString(1);
                    usuarioMatch.Apellido = reader.GetString(2);
                    usuarioMatch.NombreUsuario = reader.GetString(3);
                    usuarioMatch.Password = reader.GetString(4);
                    usuarioMatch.Mail = reader.GetString(5);
                    Console.WriteLine("Bienvenido !!!");
                }
                else
                {
                    Console.WriteLine("Usuario y/o Contraseña inválidos");
                }
            }
            return usuarioMatch;
        }

        // ELIMINAR USUARIO 
        public static int DeleteUsuario(long id)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                { 
                    SqlCommand comando = new SqlCommand($"DELETE FROM Usuario WHERE Id = @id", conexion);
                    conexion.Open();
                    comando.Parameters.AddWithValue("id", id);
                    comando.ExecuteNonQuery();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("" + e.Message);
                    return -1;
                }
            }
        }
    }
}


