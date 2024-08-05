using ClientesMs.Dtos;
using ClientesMs.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace ClientesMs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRdN _cliente;

        public ClientesController(ClienteRdN cliente)
        {
            _cliente = cliente;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAsync(ClienteDto dto)
        {
            int id;

            id = await _cliente.AgregarAsync(dto);

            return Created("", new { id });
        }
    }
}