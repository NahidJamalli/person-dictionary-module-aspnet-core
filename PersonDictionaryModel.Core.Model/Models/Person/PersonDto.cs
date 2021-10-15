using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Model.Models.Person;
using System;
using System.Collections.Generic;

namespace PersonDictionaryModel.Core.Model.Models
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public string TargetUrl { get; set; }

        public DateTime BirthDate { get; set; }

        public int? RelatedPeopleCount => RelatedPeople?.Count;

        public CityDto City { get; set; }

        public ICollection<RelatedPersonDto> RelatedPeople { get; set; }

        public ICollection<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}
