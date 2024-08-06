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
            string id;

            id = await _cliente.AgregarAsync(dto);

            return Created("", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosAsync()
        {
            var lista = await _cliente.ObtenerTodosAsync();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorIdEncodedKeyAsync(string id)
        {
            var item = await _cliente.ObtenerPorIdAsync(id);
            if (item == null)
                return NotFound(new { Mensaje = "Cliente no encontrado" });

            return Ok(item);
        }
    }
}