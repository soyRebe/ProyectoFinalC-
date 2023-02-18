using MiPrimeraAppV1.Models;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace MiPrimeraAppV1.Repositories
{
    internal static class ManejadorVenta
    {

        public static string connetionString = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=rechegoy_sistema_gestion;" +
            "User Id=rechegoy_sistema_gestion;" +
            "Password=Mp28185134;";
        public static List<Venta> ObtenerVentas(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Venta WHERE @IdUsuario = idUsuario", conn);
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Venta ventaTemp = new Venta();
                        ventaTemp.Id = reader.GetInt64(0);
                        ventaTemp.Comentarios = reader.GetString(1);
                        ventaTemp.IdUsuario = reader.GetInt64(2);
                        ventas.Add(ventaTemp);
                    }
                }

                return ventas;

            }

        }

        public static void CargarVenta(long idUsuario , List<Producto> productosVendidos)
        {
            Venta venta = new Venta();
            using (SqlConnection conn = new SqlConnection(connetionString))
            {

                SqlCommand comando = new SqlCommand();
                comando.Connection = conn;
                comando.Connection.Open();

                venta.Comentarios = "";
                venta.IdUsuario = idUsuario;
                venta.Id = InsertarVenta(venta);

                foreach (Producto producto in productosVendidos)
                {
                    ProductoVendido productoVendido = new ProductoVendido();
                    productoVendido.Stock = producto.Stock;
                    productoVendido.IdProducto = producto.Id;
                    productoVendido.IdVenta = venta.Id;

                    ManejadorProductoVendido.InsertarProductoVendido( productoVendido );

                    /*comando.CommandText = @"INSERT INTO ProductoVendido ([Stock], [IdProducto],[IdVenta] ) VALUES( @stock, @idProducto, @idVenta)";

                    comando.Parameters.AddWithValue("@stock", producto.Stock);
                    comando.Parameters.AddWithValue("@idProducto", producto.Id);
                    comando.Parameters.AddWithValue("@idVenta", venta.Id);
                    comando.ExecuteNonQuery();*/


                    /* comando.CommandText = @"UPDATE Producto SET [Stock]=[Stock] - @stock WHERE [Id]=@idProducto";

                    comando.Parameters.AddWithValue("@stock", producto.Stock);
                    comando.Parameters.AddWithValue("@idProducto", producto.Id);
                    comando.ExecuteNonQuery(); */

                    ManejadorProducto.UpdateStockProducto(productoVendido.IdProducto, productoVendido.Stock);


                }
               // comando.Connection.Close();

            }

        }

        public static long InsertarVenta(Venta venta)
        {
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                
                SqlCommand comando = new SqlCommand();

                comando.Connection = conn;
                comando.Connection.Open();

                comando.CommandText = "INSERT INTO Venta ([Comentarios], [IdUsuario]) VALUES( @comentarios, @idUsuario)";
                comando.Parameters.AddWithValue("@comentarios", venta.Comentarios);
                comando.Parameters.AddWithValue("@idUsuario", venta.IdUsuario);               
                comando.ExecuteNonQuery();

                comando.CommandText = "SELECT @@Identity";
                long LastID = Convert.ToInt64(comando.ExecuteScalar());
                comando.Connection.Close();
                

                return LastID;

            }

          
        }



    }
}
