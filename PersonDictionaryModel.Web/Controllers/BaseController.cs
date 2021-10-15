using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PersonDictionaryModel.Web.Filters;

namespace PersonDictionaryModel.Web.Controllers
{
    [ServiceFilter(typeof(GlobalExceptionFilterAttribute), IsReusable = true)]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private protected readonly IMemoryCache _memoryCache;

        public BaseController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
    }
}
