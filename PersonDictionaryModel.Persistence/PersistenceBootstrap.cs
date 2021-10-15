using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PersonDictionaryModel.Persistence
{
    public static class PersistenceBootstrap
    {
        public const string MigrationsHistoryTableName = "__EFMigrationsHistory_PersonDictionaryModel";

        public static IServiceCollection AddPersonDictionaryModel(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersonDictionaryModelContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("PersonDictionaryModelConnection"),
                    builder => builder.MigrationsHistoryTable(MigrationsHistoryTableName)));
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersonDictionaryModel(configuration);

            return services;
        }

    }
}
