using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PersonDictionaryModel.Core.Application.Interfaces;
using PersonDictionaryModel.Core.Model.Enums;
using PersonDictionaryModel.Core.Model.Exceptions;
using PersonDictionaryModel.Core.Model.Models;
using PersonDictionaryModel.Persistence;
using PersonDictionaryModel.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityDomainModel = PersonDictionaryModel.Core.Domain.Models.City;

namespace PersonDictionaryModel.Core.Infrastructure.City
{
    public sealed class CityRepository : ICityRepository
    {
        private readonly PersonDictionaryModelContext _dbContext;
        private readonly IMapper _mapper;

        public CityRepository(PersonDictionaryModelContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task AddAsync(CityDomainModel entityToAdd)
        {
            await _dbContext.Cities.AddAsync(entityToAdd);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<EntityStatus> DeleteAsync(int entityId)
        {
            var cityFromDb = await _dbContext.Cities.FirstOrDefaultAsync(x => x.Id == entityId);

            if (cityFromDb is null)
                throw new PersonDictionaryModelException(ApiErrorCodeKeys.E10004, nameof(Domain.Models.City));

            _dbContext.Cities.Remove(cityFromDb);

            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(EntityStatus.Success);
        }

        public async Task<List<CityDto>> GetAllAsync()
        {
            return await _dbContext
                .Cities
                .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public Task UpdateAsync(CityDomainModel entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
