using PersonDictionaryModel.Application.Interfaces;
using PersonDictionaryModel.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonDictionaryModel.Core.Application.Interfaces
{
    public interface IRelatedPersonRepository : IRepository<RelatedPerson>
    {
        Task AddRangeAsync(int personId, List<RelatedPerson> entiesToAdd);
        Task UpdateAsync(RelatedPerson entityToUpdate);
    }
}
