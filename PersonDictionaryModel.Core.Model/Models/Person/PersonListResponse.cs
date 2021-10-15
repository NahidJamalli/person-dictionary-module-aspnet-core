using System.Collections.Generic;

namespace PersonDictionaryModel.Core.Model.Models.Person
{
    public class PersonListResponse
    {
        public List<PersonDto> PeopleData { get; set; }

        public int TotalPeopleCount { get; set; }
    }
}
