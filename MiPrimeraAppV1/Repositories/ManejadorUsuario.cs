using MiPrimeraAppV1.Models;
using System.Data.SqlClient;

namespace MiPrimeraAppV1.Repositories
{
    internal static class ManejadorUsuario
    {

        public static string connetionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=rechegoy_sistema_gestion;" +
            "User Id=rechegoy_sistema_gestion;" +
            "Password=Mp28185134;";
        public static Usuario ObtenerUsuario(long id)
        {

            Usuario usuario = new Usuario();
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE id=@id", conn);
                comando.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    Usuario usuario1 = new Usuario();
                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }
            }

            return usuario;
        }

        public static Usuario ObtenerUsuarioLogin(string usuario, string contraseña)
        {
            Usuario usuarioLogin = new Usuario();

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE @NombreUsuario = NombreUsuario and @Contraseña = Contraseña", conn);
                comando.Parameters.AddWithValue("@NombreUsuario", usuario);
                comando.Parameters.AddWithValue("@Contraseña", contraseña);

                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    Usuario usuario1 = new Usuario();
                    usuario1.Id = reader.GetInt64(0);
                    usuario1.Nombre = reader.GetString(1);
                    usuario1.Apellido = reader.GetString(2);
                    usuario1.NombreUsuario = reader.GetString(3);
                    usuario1.Contraseña = reader.GetString(4);
                    usuario1.Mail = reader.GetString(5);

                    usuarioLogin = usuario1;
                }
            }

            return usuarioLogin;
        }


        public static Usuario ObtenerNombreUsuario(string usuario)
        {
            Usuario userName = new Usuario();

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE @NombreUsuario = NombreUsuario", conn);
                comando.Parameters.AddWithValue("@NombreUsuario", usuario);
            

                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Usuario usuarioNombre = new Usuario();
                    usuarioNombre.Id = reader.GetInt64(0);
                    usuarioNombre.Nombre = reader.GetString(1);
                    usuarioNombre.Apellido = reader.GetString(2);
                    usuarioNombre.NombreUsuario = reader.GetString(3);
                    userName = usuarioNombre;

                }

                return userName;
            }

            
        }




        public static Usuario InsertarUsuario(Usuario usuario)
        {

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();
                comando.CommandText = @"INSERT INTO Usuario([Nombre],[Apellido], [NombreUsuario], [Contraseña], [Mail]) VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail)";
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return usuario;
        }


        public static Usuario ModificarUsuario(Usuario usuario)
        {

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();
                comando.CommandText = @"UPDATE Usuario
                                       SET [Nombre] = @nombre,
                                           [Apellido] =  @apellido,
                                           [NombreUsuario] =  @nombreUsuario,
                                           [Contraseña] =  @contraseña,
                                           [Mail] = @mail
                                       WHERE id = @ID"; 

                             
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);
                comando.Parameters.AddWithValue("@ID", usuario.id);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return usuario;
        }

        public static long EliminarUsuario( long id)
        {
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();
                comando.CommandText = @"DELETE
                                            [Usuario]
                                        WHERE 
                                            [Id]=@ID";

                comando.Parameters.AddWithValue("@ID", id);
                comando.ExecuteNonQuery();

       
                comando.Connection.Close();
            }

            return id;
        }



    }
}
