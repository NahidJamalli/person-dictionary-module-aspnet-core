using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PersonDictionaryModel.Application.Interfaces;
using PersonDictionaryModel.Core.Application.Interfaces;
using PersonDictionaryModel.Core.Model.Enums;
using PersonDictionaryModel.Core.Model.Exceptions;
using PersonDictionaryModel.Core.Model.Models;
using PersonDictionaryModel.Core.Model.Models.Person;
using PersonDictionaryModel.Core.Model.Resourcearameters;
using PersonDictionaryModel.Persistence;
using PersonDictionaryModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonDomainModel = PersonDictionaryModel.Core.Domain.Models.Person;
using PhoneNumberDomainModel = PersonDictionaryModel.Core.Domain.Models.PhoneNumber;
using RelatedPersonDomainModel = PersonDictionaryModel.Core.Domain.Models.RelatedPerson;

namespace PersonDictionaryModel.Core.Infrastructure.Person
{
    public sealed class PersonRepository : IPersonRepository
    {
        private readonly PersonDictionaryModelContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPhoneNumberRepository _phoneNumberRepo;
        private readonly IRelatedPersonRepository _relatedPersonRepo;


        public PersonRepository(
            PersonDictionaryModelContext context,
            IMapper mapper,
            IPhoneNumberRepository phoneNumberRepo,
            IRelatedPersonRepository relatedPersonRepo)
        {
            _dbContext = context;
            _mapper = mapper;
            _phoneNumberRepo = phoneNumberRepo;
            _relatedPersonRepo = relatedPersonRepo;
        }

        public async Task<PersonDto> AddAsync(PersonDomainModel entityToAdd)
        {
            var relatedPeople = entityToAdd.RelatedPeople;
            var numbers = entityToAdd.PhoneNumbers;

            var cityFromDb = await _dbContext
                                    .Cities
                                    .FirstOrDefaultAsync(x => x.Id == entityToAdd.CityId);

            if (cityFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(Domain.Models.City));

            entityToAdd.PhoneNumbers = null;
            entityToAdd.RelatedPeople = null;

            await _dbContext.People.AddAsync(entityToAdd);

            await _dbContext.SaveChangesAsync();

            await _phoneNumberRepo.AddRangeAsync(entityToAdd.Id, new List<PhoneNumberDomainModel>(numbers));

            await _relatedPersonRepo.AddRangeAsync(entityToAdd.Id, new List<RelatedPersonDomainModel>(relatedPeople));

            return await GetByIdAsync(entityToAdd.Id);
        }

        public async Task<EntityStatus> DeleteAsync(int personId)
        {
            var personFromDb = await _dbContext.People.SingleOrDefaultAsync(x => x.Id == personId);

            if (personFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(Domain.Models.Person));

            _dbContext.People.Remove(personFromDb);

            await _dbContext.SaveChangesAsync();

            return EntityStatus.Success;
        }

        public async Task<PersonDto> GetByIdAsync(int personId)
        {
            if (personId == 0) return new PersonDto();

            var personFromDb = await _dbContext.People
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == personId);

            if (personFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(Domain.Models.Person));

            return personFromDb;
        }

        public async Task<PersonListResponse> SearchAsync(ResourceParameter resourceParameter)
        {
            if (resourceParameter is null) throw new ArgumentNullException(nameof(resourceParameter));

            var pagedQuery = _dbContext.People as IQueryable<PersonDomainModel>;

            var namePattern = resourceParameter?.FirstName is null ? String.Empty : $"%{resourceParameter.FirstName}%";
            var lastNamePattern = resourceParameter?.LastName is null ? String.Empty : $"%{resourceParameter.LastName}%";
            var personalPhoneNumber = resourceParameter?.PersonalNumber is null ? String.Empty : $"%{resourceParameter.PersonalNumber}%";

            if (!string.IsNullOrEmpty(namePattern))
                pagedQuery = pagedQuery.Where(x => EF.Functions.Like(x.FirstName, namePattern));

            if (!string.IsNullOrEmpty(lastNamePattern))
                pagedQuery = pagedQuery.Where(x => EF.Functions.Like(x.LastName, lastNamePattern));

            if (!string.IsNullOrEmpty(personalPhoneNumber))
                pagedQuery = pagedQuery.Where(x => EF.Functions.Like(x.PersonalNumber, personalPhoneNumber));

            var peopleCount = await _dbContext.People.CountAsync();

            var resultFromDb = await pagedQuery
                 .Skip(resourceParameter.Skip)
                 .Take(resourceParameter.Take)
                 .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();

            return new PersonListResponse()
            {
                PeopleData = resultFromDb,
                TotalPeopleCount = peopleCount
            };
        }

        public async Task<PersonDto> UpdateAsync(PersonDomainModel entityToUpdate)
        {
            var relatedPeople = entityToUpdate.RelatedPeople;
            var numbers = entityToUpdate.PhoneNumbers;

            var cityFromDb = await _dbContext
                                    .Cities
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == entityToUpdate.CityId);

            if (cityFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(Domain.Models.City));

            var personFromDb = await _dbContext
                                    .People
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == entityToUpdate.Id);

            if (personFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(Domain.Models.Person));

            entityToUpdate.PhoneNumbers = null;
            entityToUpdate.RelatedPeople = null;

            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            await _phoneNumberRepo.AddRangeAsync(entityToUpdate.Id, new List<PhoneNumberDomainModel>(numbers));
            await _relatedPersonRepo.AddRangeAsync(entityToUpdate.Id, new List<RelatedPersonDomainModel>(relatedPeople));

            return await GetByIdAsync(entityToUpdate.Id);
        }
    }
}
