using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPut]
        public void Actualizar([FromBody] Usuario usuario)
        {
            ManejadorUsuario.UpdateUsuario(usuario);
        }
    }
}
