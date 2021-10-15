using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Model.Models.Person;
using PersonDictionaryModel.Core.Model.Models.PhoneNumer;
using PersonDictionaryModel.Core.Model.Models.RelatedPerson;
using System;
using System.Collections.Generic;

namespace PersonDictionaryModel.Core.Model.Models
{
    public class CreatePersonDto : IPersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public string Photo { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public ICollection<CreatePhoneNumberDto> PhoneNumbers { get; set; }

        public ICollection<CreateRelatedPersonDto> RelatedPeople { get; set; }
    }
}
