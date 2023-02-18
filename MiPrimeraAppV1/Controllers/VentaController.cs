using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppV1.Models;
using MiPrimeraAppV1.Repositories;

namespace MiPrimeraAppV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        /*  [HttpPost("{idUsuario}")]
         public void InsertSale(Venta venta)
         {
             ManejadorVenta.InsertarVenta(venta);
         }*/

        [HttpGet("{idUsuario}")]
        public void GetSale(int idUsuario)
        {
            ManejadorVenta.ObtenerVentas(idUsuario);
        }


        [HttpPost("{idUsuario}")]
        public void InsertSale(List<Producto> productos, int idUsuario)
        {
            ManejadorVenta.CargarVenta(idUsuario, productos);
        }
    }
}
