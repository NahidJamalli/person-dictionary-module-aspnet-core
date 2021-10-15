using PersonDictionaryModel.Application.Interfaces;
using PersonDictionaryModel.Core.Domain.Models;
using PersonDictionaryModel.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonDictionaryModel.Core.Application.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<List<CityDto>> GetAllAsync();
        Task AddAsync(City entityToAdd);
        Task UpdateAsync(City entityToUpdate);
    }
}
