using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PersonDictionaryModel.Core.Application.Interfaces;
using static PersonDictionaryModel.Core.Model.Constants.InMemoryCacheConstants;
using PersonDictionaryModel.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonDictionaryModel.Core.Application.Utils;

namespace PersonDictionaryModel.Web.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly ICityRepository _cityRepo;

        public CitiesController(ICityRepository cityRepo, IMemoryCache memoryCache) : base(memoryCache)
        {
            _cityRepo = cityRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCityList()
        {
            if (!_memoryCache.TryGetValue(CITIES_CACHE_KEY, out List<CityDto> cities))
            {
                cities = await _cityRepo.GetAllAsync();

                _memoryCache.Set(CITIES_CACHE_KEY, cities, InMemoryCache.MemoryCacheEntryOptions);
            }

            return Ok(cities);
        }
    }
}
