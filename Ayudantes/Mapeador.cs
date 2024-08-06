using AutoMapper;
using ClientesMs.Dtos;
using ClientesMs.Entidades;

namespace ClientesMs.Ayudantes
{
    public class Mapeador : Profile
    {
        public Mapeador()
        {
            CreateMap<DireccionDto, Direccion>().ReverseMap();

            CreateMap<ClienteDtoIn, Cliente>();

            CreateMap<Cliente, ClienteDto>();
        }
    }
}