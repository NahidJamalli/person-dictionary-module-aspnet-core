using Microsoft.Extensions.DependencyInjection;
using PersonDictionaryModel.Core.Application.Interfaces;
using PersonDictionaryModel.Core.Application.Services;
using System;

namespace PersonDictionaryModel.Core.Bootstrap
{
    public static class ApplicationBootstrap
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IFileService, FileService>();
        }
    }
}
