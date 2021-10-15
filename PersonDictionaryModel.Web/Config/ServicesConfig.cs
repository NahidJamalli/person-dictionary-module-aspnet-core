using Microsoft.Extensions.DependencyInjection;
using PersonDictionaryModel.Web.Filters;

namespace PersonDictionaryModel.Web.Config
{
    public static class ServicesConfig
    {
        public static void Initialize(this IServiceCollection services)
        {
            services.AddSingleton<GlobalExceptionFilterAttribute>();
            services.AddSingleton<ValidatePersonModelAttribute>();
        }
    }
}
