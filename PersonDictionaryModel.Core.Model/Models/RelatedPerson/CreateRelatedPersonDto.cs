using PersonDictionaryModel.Core.Domain.Enums;

namespace PersonDictionaryModel.Core.Model.Models.RelatedPerson
{
    public class CreateRelatedPersonDto
    {
        public RelativeType RelativeType { get; set; }

        public int PersonId { get; set; }
    }
}
