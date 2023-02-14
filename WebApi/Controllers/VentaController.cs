using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("{idusuario}")]
        public void CrearVenta(List<Producto> productos, long idUsuario)
        {
            ManejadorVentas.InsertarVenta(productos, idUsuario);
        }
    }
}
