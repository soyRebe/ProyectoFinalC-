namespace MiPrimeraAppV1.Models
{
    public class ProductoVendido
    {
        public int id;
        public int stock;
        public long idProducto;
        public long idVenta;

        public int Id { get; set; }
        public int Stock { get; set; }
        public long IdProducto { get; set; }
        public long IdVenta { get; set; }
    }
}
