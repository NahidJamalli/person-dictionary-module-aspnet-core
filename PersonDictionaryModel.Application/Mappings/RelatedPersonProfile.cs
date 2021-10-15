using AutoMapper;
using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Core.Model.Models.Person;
using PersonDictionaryModel.Core.Model.Models.RelatedPerson;

namespace PersonDictionaryModel.Core.Application.Mappings
{
    public class RelatedPersonProfile : Profile
    {
        public RelatedPersonProfile()
        {
            CreateMap<RelatedPerson, CreateRelatedPersonDto>().ReverseMap();

            CreateMap<RelatedPerson, RelatedPersonDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x=> x.Person.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x=> x.Person.LastName))
                .ReverseMap();
        }
    }
}
