using PersonDictionaryModel.Core.Domain.Enums;

namespace PersonDictionaryModel.Core.Model.Models.Person
{
    public class RelatedPersonDto
    {
        public RelativeType RelativeType { get; set; }

        public int PersonId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
