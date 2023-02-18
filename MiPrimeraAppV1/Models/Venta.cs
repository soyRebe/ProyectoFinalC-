namespace MiPrimeraAppV1.Models
{
    public class Venta
    {

        private long id;
        private string comentarios;
        private long idUsuario;

        public long Id { get => id; set => id = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public long IdUsuario { get => idUsuario; set => idUsuario = value; }
    }
}
