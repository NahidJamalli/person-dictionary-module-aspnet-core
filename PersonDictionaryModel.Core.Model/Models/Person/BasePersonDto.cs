using Microsoft.AspNetCore.Http;
using PersonDictionaryModel.Core.Domain.Enums;
using System;

namespace PersonDictionaryModel.Core.Model.Models.Person
{
    public interface IPersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public string Photo { get; set; }

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }
    }
}
