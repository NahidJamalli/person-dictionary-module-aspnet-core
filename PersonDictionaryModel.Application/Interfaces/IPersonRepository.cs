using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Core.Model.Models;
using PersonDictionaryModel.Core.Model.Models.Person;
using PersonDictionaryModel.Core.Model.Resourcearameters;
using System.Threading.Tasks;

namespace PersonDictionaryModel.Application.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<PersonDto> AddAsync(Person personToAdd);
        Task<PersonDto> GetByIdAsync(int personId);
        Task<PersonListResponse> SearchAsync(ResourceParameter resourceParameter);
        Task<PersonDto> UpdateAsync(Person entityToUpdate);
    }
}
