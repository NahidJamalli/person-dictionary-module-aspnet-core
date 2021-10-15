using PersonDictionaryModel.Core.Domain.Interfaces;
using PersonDictionaryModel.Core.Model.Enums;
using System.Threading.Tasks;

namespace PersonDictionaryModel.Application.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<EntityStatus> DeleteAsync(int entityId);
    }
}
