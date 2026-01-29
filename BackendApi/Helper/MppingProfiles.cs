using AutoMapper;
using PokemonReviewApp.Dto;
using WEBAPIDEMO.Dto;
using WEBAPIDEMO.Models;

namespace WEBAPIDEMO.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon , PokemonDto>().ReverseMap();
            CreateMap<Category ,CategoryDto>().ReverseMap();
            CreateMap<Country ,CountryDto>().ReverseMap();
            CreateMap<Owner , OwnerDto>().ReverseMap();
        }
    }}