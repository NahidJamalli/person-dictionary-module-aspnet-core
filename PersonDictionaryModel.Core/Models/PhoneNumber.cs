using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Domain.Interfaces;

namespace PersonDictionaryModel.Core.Domain.Models
{
    public sealed class PhoneNumber : IEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public PhoneNumberType Type { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
