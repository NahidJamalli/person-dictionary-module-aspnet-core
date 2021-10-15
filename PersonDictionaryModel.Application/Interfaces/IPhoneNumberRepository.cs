using PersonDictionaryModel.Application.Interfaces;
using PersonDictionaryModel.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonDictionaryModel.Core.Application.Interfaces
{
    public interface IPhoneNumberRepository : IRepository<PhoneNumber>
    {
        Task UpdateAsync(PhoneNumber entityToUpdate);
        Task AddRangeAsync(int personId, List<PhoneNumber> numbersToAddOrUpdate);
    }
}