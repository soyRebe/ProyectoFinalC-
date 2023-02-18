using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraAppV1.Models;
using MiPrimeraAppV1.Repositories;

namespace MiPrimeraAppV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {


        [HttpGet("{idUsuario}")]
        public List<Producto> GetSaleProductByUserId(long idUsuario)
        {
            
            return ManejadorProductoVendido.ObtenerProductoVendido(idUsuario);
        }

    }

}

