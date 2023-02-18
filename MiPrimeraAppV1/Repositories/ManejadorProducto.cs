using MiPrimeraAppV1.Models;
using System.Data.SqlClient;

namespace MiPrimeraAppV1.Repositories
{
     internal static class ManejadorProducto
    {
        public static string connetionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=rechegoy_sistema_gestion;" +
            "User Id=rechegoy_sistema_gestion;" +
            "Password=Mp28185134;";
        public static List<Producto> ObtenerProductos(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE @IdUsuario = idUsuario", conn);
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
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


        public static Producto ObtenerProducto(long id)
        {
            Producto producto = new Producto();
            //string cadenaConexion = "Data Source = DESKTOP-SUJUNQM; Initial Catalog = SISTEMADEGESTION; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                // Forma 1 para crear un comando con parametros
                //SqlCommand comando = new SqlCommand($"SELECT * FROM Producto WHERE Descripciones = '{descripciones}' ", conn);

                // Forma 2 para crear un comando con parametros
                //SqlCommand comando2 = new SqlCommand("SELECT * FROM Producto WHERE Descripciones = @Descripciones", conn);

                // Creo mi parametro de descripciones
                //SqlParameter DescriptionParameter = new SqlParameter();
                //DescriptionParameter.Value = descripciones;
                //IdParameter.SqlDbType = SqlDbType.VarChar;
                //DescriptionParameter.ParameterName= "Descripciones";

                //comando2.Parameters.Add(DescriptionParameter);

                // Forma 3 para crear un comando con parametros

                SqlCommand comando2 = new SqlCommand("SELECT * FROM Producto WHERE id=@Id", conn);
                comando2.Parameters.AddWithValue("@Id", id);

                conn.Open();

                SqlDataReader reader = comando2.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    Producto productoTemporal = new Producto();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);

                }
                return producto;
            }
        }

        public static List<Producto> ObtenerProductoVendido(long idUsuario)
        {
            List<long> ListaIdProductos = new List<long>();

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando3 = new SqlCommand("SELECT IdProducto FROM Venta INNER JOIN ProductoVendido  ON venta.id = ProductoVendido.IdVenta WHERE IdUsuario = @idUsuario", conn);

                comando3.Parameters.AddWithValue("@idUsuario", idUsuario);

                conn.Open();

                SqlDataReader reader = comando3.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ListaIdProductos.Add(reader.GetInt64(0));
                    }
                }
            }
            List<Producto> productos = new List<Producto>();
            foreach (var id in ListaIdProductos)
            {
                Producto prodTemp = ObtenerProducto(id);
                productos.Add(prodTemp);
            }

            return productos;

        }


        public static Producto InsertarProducto(Producto producto)
        {

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();
                comando.CommandText = @"INSERT INTO Producto([Descripciones],[Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario)";
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return producto;
        }


        public static Producto ModificarProducto(Producto producto)
        {

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();
                comando.CommandText = @"UPDATE Producto
                                        SET [Descripciones]= @descripciones,
                                            [Costo]= @costo, 
                                            [PrecioVenta]= @precioVenta, 
                                            [Stock]=@stock
                                        WHERE [Id]=@ID";
               
                
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@ID", producto.Id);
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return producto;
        }

        public static int EliminarProducto(int id)
        {

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();
                comando.CommandText = @"DELETE
                                            [ProductoVendido]
                                        WHERE 
                                            [IdProducto]=@ID";

                comando.Parameters.AddWithValue("@ID", id);
                comando.ExecuteNonQuery();

                comando.CommandText = @"DELETE
                                            [Producto]
                                        WHERE 
                                            [Id]=@ID";
              
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }

            return id;
        }


        public static Producto UpdateStockProducto(long id, int cantidadVendidos)
        {
            Producto producto = ObtenerProducto(id);
            producto.Stock -= cantidadVendidos;
            return ModificarProducto(producto);
        }
    }
}
