using AutoMapper;
using PRIMERA_API.Data.Dto;
using PRIMERA_API.Data.Models;

namespace PRIMERA_API.Data
{
    public class ConfiguracionAutoMapper : Profile
    {
        public ConfiguracionAutoMapper()
        {
            CreateMap<ClienteCrearDto, Cliente>().ReverseMap();
            CreateMap<ClienteSelectDto, Cliente>().ReverseMap();
            CreateMap<ClienteUpdateDto, Cliente>().ReverseMap();

            CreateMap<AlquilerCrearDto, Alquiler>().ReverseMap();
            CreateMap<AlquilerSelectDto, Alquiler>().ReverseMap();
            CreateMap<AlquilerUpdateDto, Alquiler>().ReverseMap();

            CreateMap<TipovehiculoCrearDto, Tipovehiculo>().ReverseMap();
            CreateMap<TipovehiculoSelectDto, Tipovehiculo>().ReverseMap();
            CreateMap<TipovehiculoUpdateDto, Tipovehiculo>().ReverseMap();
        }
    }
}
