using AnimalAPI.Database;
using AnimalAPI.DTO;
using AutoMapper;

namespace AnimalAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AnimalCreateDto, Animal>().ReverseMap();
            CreateMap<AnimalDto, Animal>().ReverseMap();
        }
    }
}
