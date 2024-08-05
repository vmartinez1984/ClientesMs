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
        
        public async Task<int> AgregarAsync(ClienteDto dto)
        {
            int id;
            Cliente  cliente;

            cliente = _mapper.Map<Cliente>(dto);
            id = await _repositorio.AgregarAsync(cliente);

            return id;
        }
    }
}