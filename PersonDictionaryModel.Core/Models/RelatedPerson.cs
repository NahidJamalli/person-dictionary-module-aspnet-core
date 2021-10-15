using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Domain.Interfaces;

namespace PersonDictionaryModel.Core.Domain.Models
{
    public sealed class RelatedPerson : IEntity
    {
        public int Id { get; set; }
        public RelativeType RelativeType { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
