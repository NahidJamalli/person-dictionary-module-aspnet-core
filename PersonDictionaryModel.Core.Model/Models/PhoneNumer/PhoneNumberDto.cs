using PersonDictionaryModel.Core.Domain.Enums;

namespace PersonDictionaryModel.Core.Model.Models
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }

        public PhoneNumberType Type { get; set; }

        public string Number { get; set; }

        public int PersonId { get; set; }
    }
}
