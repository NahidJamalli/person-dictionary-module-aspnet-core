using Microsoft.EntityFrameworkCore;
using PersonDictionaryModel.Core.Application.Interfaces;
using PersonDictionaryModel.Core.Model.Enums;
using PersonDictionaryModel.Core.Model.Exceptions;
using PersonDictionaryModel.Persistence;
using PersonDictionaryModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelatedPersonModel = PersonDictionaryModel.Core.Domain.Models.RelatedPerson;

namespace PersonDictionaryModel.Core.Infrastructure.RelatedPerson
{
    public sealed class RelatedPersonRepository : IRelatedPersonRepository
    {
        private readonly PersonDictionaryModelContext _dbContext;

        public RelatedPersonRepository(PersonDictionaryModelContext context)
        {
            _dbContext = context;
        }

        public async Task AddRangeAsync(int personId, List<RelatedPersonModel> entiesToAdd)
        {
            var oldRelatedPeople = await _dbContext
                .RelatedPeople
                .Where(x => x.PersonId == personId)
                .ToListAsync();

            entiesToAdd.Select(c => { c.PersonId = personId; c.Id = default; c.Person = null; return c; }).ToList();

            _dbContext.RemoveRange(oldRelatedPeople);

            await _dbContext.RelatedPeople.AddRangeAsync(entiesToAdd);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<EntityStatus> DeleteAsync(int entityId)
        {
            var relatedPersonFromDb = await _dbContext.RelatedPeople.SingleOrDefaultAsync(x => x.Id == entityId);

            if (relatedPersonFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(RelatedPerson));

            _dbContext.RelatedPeople.Remove(relatedPersonFromDb);

            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(EntityStatus.Success);
        }

        public Task UpdateAsync(RelatedPersonModel entityToUpdate) =>
            throw new NotImplementedException();
    }
}
