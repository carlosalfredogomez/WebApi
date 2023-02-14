using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpPost]
        public void IngresarProducto(Producto producto)
        {
            ManejadorProducto.InsertarProducto(producto);
        }

        [HttpPut]
        public void Actualizar([FromBody] Producto producto)
        {
            ManejadorProducto.UpdateProducto(producto);
        }

        [HttpDelete("{id}")]
        public void EliminarProducto(long id)
        {
            ManejadorProducto.DeleteProducto(id);
        }
    }
}
