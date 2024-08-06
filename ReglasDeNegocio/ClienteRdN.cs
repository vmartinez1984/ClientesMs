using AutoMapper;
using ClientesMs.Dtos;
using ClientesMs.Entidades;
using ClientesMs.Repositorios;

namespace ClientesMs.ReglasDeNegocio
{
    public class ClienteRdN
    {
        private readonly RepositorioDeCliente _repositorio;
        private readonly IMapper _mapper;

        public ClienteRdN(RepositorioDeCliente repositorio,IMapper mapper )
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        
        public async Task<string> AgregarAsync(ClienteDto dto)
        {
            string id;
            Cliente  cliente;

            cliente = _mapper.Map<Cliente>(dto);
            id = await _repositorio.AgregarAsync(cliente);

            return id;
        }

        public async Task<List<ClienteDto>> ObtenerTodosAsync()
        {
            List<ClienteDto> dtos;
            List<Cliente> clientes;

            clientes = await _repositorio.ObtenerTodosAsync();
            dtos = _mapper.Map<List<ClienteDto>>(clientes);

            return dtos;            
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