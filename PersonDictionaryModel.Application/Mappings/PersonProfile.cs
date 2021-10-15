using AutoMapper;
using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Core.Model.Models;

namespace PersonDictionaryModel.Core.Application.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, CreatePersonDto>().ReverseMap();
            CreateMap<Person, UpdatePersonDto>().ReverseMap();
            CreateMap<Person, PersonDto>()
                .ForMember(x=> x.TargetUrl, opt => opt.MapFrom(x=> x.TargetUrl))
                .ReverseMap();
        }
    }
}
