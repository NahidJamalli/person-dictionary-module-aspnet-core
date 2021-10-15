using Microsoft.EntityFrameworkCore;
using PersonDictionaryModel.Core.Application.Interfaces;
using PersonDictionaryModel.Core.Model.Enums;
using PersonDictionaryModel.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneNumerDomainModel = PersonDictionaryModel.Core.Domain.Models.PhoneNumber;

namespace PersonDictionaryModel.Core.Infrastructure.PhoneNumber
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {

        private readonly PersonDictionaryModelContext _dbContext;

        public PhoneNumberRepository(PersonDictionaryModelContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeAsync(int personId, List<PhoneNumerDomainModel> numbersToAddOrUpdate)
        {
            var oldPhoneNumbers = await _dbContext
                .PhoneNumbers
                .Where(x => x.PersonId == personId)
                .ToListAsync();

            numbersToAddOrUpdate.Select(c => { c.PersonId = personId; c.Id = default; c.Person = null; return c; }).ToList();

            _dbContext.PhoneNumbers.RemoveRange(oldPhoneNumbers);
            await _dbContext.PhoneNumbers.AddRangeAsync(numbersToAddOrUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public Task<EntityStatus> DeleteAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PhoneNumerDomainModel entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
