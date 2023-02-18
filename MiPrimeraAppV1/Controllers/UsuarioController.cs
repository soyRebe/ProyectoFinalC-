using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppV1.Models;
using MiPrimeraAppV1.Repositories;

namespace MiPrimeraAppV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario GetUserNameByUserNameAndMail(string nombreUsuario, string contraseña)
        {
            var usuario = ManejadorUsuario.ObtenerUsuarioLogin(nombreUsuario, contraseña);
            return usuario == null ? new Usuario() : usuario;
        }

        [HttpGet("{nombreUsuario}")]
        public Usuario GetUserNameByUserName(string nombreUsuario)
        {
            var usuario = ManejadorUsuario.ObtenerNombreUsuario(nombreUsuario);
            return usuario == null ? new Usuario() : usuario;
        }


        [HttpPost]
        public void InsertUSer(Usuario usuario)
        {
             ManejadorUsuario.InsertarUsuario(usuario);
        }

        [HttpPut]
        public void UpdateUser(Usuario usuario)
        {
            ManejadorUsuario.ModificarUsuario(usuario);
        }

        [HttpDelete("{id}")]
        public void deleteUSer(long id)
        {
            ManejadorUsuario.EliminarUsuario(id);  
        }



    }
}