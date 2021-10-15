using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace PersonDictionaryModel.Core.Domain.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public string TargetUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<RelatedPerson> RelatedPeople { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
