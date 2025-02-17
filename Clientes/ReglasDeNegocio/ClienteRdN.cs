using AutoMapper;
using ClientesMs.Dtos;
using ClientesMs.Entidades;
using ClientesMs.Repositorios;

namespace ClientesMs.ReglasDeNegocio
{
    /// <summary>
    /// Reglas de negocio de clientes
    /// </summary>
    public class ClienteRdN
    {
        private readonly RepositorioDeCliente _repositorio;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inyeccion del maper y el repositorio
        /// </summary>
        /// <param name="repositorio"></param>
        /// <param name="mapper"></param>
        public ClienteRdN(RepositorioDeCliente repositorio,IMapper mapper )
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Agregar cliente
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> AgregarAsync(ClienteDto dto)
        {
            string id;
            Cliente  cliente;

            cliente = _mapper.Map<Cliente>(dto);
            id = await _repositorio.AgregarAsync(cliente);

            return id;
        }

        /// <summary>
        /// Obtener todos los clientes
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            List<ClienteDto> dtos;
            List<Cliente> clientes;

            clientes = await _repositorio.ObtenerTodosAsync();
            dtos = _mapper.Map<List<ClienteDto>>(clientes);

            return dtos;            
        }

        internal async Task ActualizarAsync(string id, ClienteDtoIn cliente)
        {
            Cliente clienteAnterior;

            clienteAnterior = await _repositorio.ObtenerPorIdAsync(id);
            clienteAnterior = _mapper.Map(cliente, clienteAnterior);

            await _repositorio.ActualizarAsync(clienteAnterior);
        }

        internal async Task<ClienteDto> ObtenerPorCorreoAsync(string correo)
        {
            ClienteDto dto;
            Cliente cliente;

            cliente = await _repositorio.ObtenerPorCorreoAsync(correo);
            dto = _mapper.Map<ClienteDto>(cliente);

            return dto;
        }

        internal async Task<ClienteDto> ObtenerPorIdAsync(string id)
        {
            ClienteDto dto;
            Cliente cliente;

            cliente = await _repositorio.ObtenerPorIdAsync(id);
            dto = _mapper.Map<ClienteDto>(cliente);

            return dto;
        }
    }
}