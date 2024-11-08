using AutoMapper;
using ClientesMs.Dtos;
using ClientesMs.Entidades;

namespace ClientesMs.Ayudantes
{
    /// <summary>
    /// Clase para meaper
    /// </summary>
    public class Mapeador : Profile
    {
        /// <summary>
        /// Mapeando las clases
        /// </summary>
        public Mapeador()
        {
            CreateMap<DireccionDto, Direccion>().ReverseMap();

            CreateMap<ClienteDtoIn, Cliente>();

            CreateMap<Cliente, ClienteDto>();
        }
    }
}