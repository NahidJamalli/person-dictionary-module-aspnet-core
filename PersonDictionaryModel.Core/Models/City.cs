using PersonDictionaryModel.Core.Domain.Interfaces;
using System.Collections.Generic;

namespace PersonDictionaryModel.Core.Domain.Models
{
    public sealed class City : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Person> People { get; set; }
    }
}
