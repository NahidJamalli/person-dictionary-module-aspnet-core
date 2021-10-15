using Microsoft.Extensions.DependencyInjection;
using PersonDictionaryModel.Application.Interfaces;
using PersonDictionaryModel.Core.Application.Interfaces;
using PersonDictionaryModel.Core.Infrastructure.City;
using PersonDictionaryModel.Core.Infrastructure.Person;
using PersonDictionaryModel.Core.Infrastructure.PhoneNumber;
using PersonDictionaryModel.Core.Infrastructure.RelatedPerson;
using System;

namespace PersonDictionaryModel.Core.Bootstrap
{
    public static class InfrastructureBootstrap
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRelatedPersonRepository, RelatedPersonRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
