using AutoMapper;
using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Core.Model.Models;

namespace PersonDictionaryModel.Core.Application.Mappings
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
