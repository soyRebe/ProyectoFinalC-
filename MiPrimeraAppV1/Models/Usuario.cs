namespace MiPrimeraAppV1.Models
{
    public class Usuario
    {

        public long id;
        public string nombre;
        public string apellido;
        public string nombreUsuario;
        public string contraseña;
        public string mail;


        public long Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Mail { get => mail; set => mail = value; }

    }
}
