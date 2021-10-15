using AutoMapper;
using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Core.Model.Models;
using PersonDictionaryModel.Core.Model.Models.PhoneNumer;

namespace PersonDictionaryModel.Core.Application.Mappings
{
    public class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<PhoneNumber, CreatePhoneNumberDto>().ReverseMap();
            CreateMap<PhoneNumber, PhoneNumberDto>()
                .ForMember(x=> x.Number, opt => opt.MapFrom(x=> x.Number))
                .ForMember(x=> x.Type, opt => opt.MapFrom(x=> x.Type))
                .ReverseMap();
        }
    }
}
