using ClientesMs.Dtos;
using ClientesMs.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace ClientesMs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRdN _cliente;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        public ClientesController(ClienteRdN cliente, ILogger<ClientesController> logger)
        {
            _cliente = cliente;
            logger.LogInformation("Iniciando");
        }

       /// <summary>
       /// Agregar clientes
       /// </summary>
       /// <param name="dto"></param>
       /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AgregarAsync(ClienteDto dto)
        {
            string id;

            id = await _cliente.AgregarAsync(dto);

            return Created("", new { id });
        }

        /// <summary>
        /// Obtener todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosAsync()
        {
            var lista = await _cliente.ObtenerTodosAsync();

            return Ok(lista);
        }

        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorIdEncodedKeyAsync(string id)
        {
            var item = await _cliente.ObtenerPorIdAsync(id);
            if (item == null)
                return NotFound(new { Mensaje = "Cliente no encontrado" });

            return Ok(item);
        }

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAsync(string id, ClienteDtoIn cliente)
        {
            var item = await _cliente.ObtenerPorIdAsync(id);
            if (item == null)
                return NotFound(new { Mensaje = "Cliente no encontrado" });
            await _cliente.ActualizarAsync(id, cliente);

            return Ok(item);
        }

        /// <summary>
        /// Obtener cliente por correo
        /// </summary>
        /// <param name="correo"></param>
        /// <returns></returns>
        [HttpGet("Correos/{correo}")]
        public async Task<IActionResult> ObtenerPorCorreo(string correo)
        {
            var item = await _cliente.ObtenerPorCorreoAsync(correo);
            if (item == null)
                return NotFound(new { Mensaje = "Cliente no encontrado" });

            return Ok(item);
        }
    }
}