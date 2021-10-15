using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PersonDictionaryModel.SharedKernel;

namespace PersonDictionaryModel.Persistence.Factories
{
    public sealed class PersonDictionaryModelFactory : DesignTimeDbContextFactoryBase<PersonDictionaryModelContext>
    {
        public PersonDictionaryModelFactory()
        {
            ConnectionStringName = "PersonDictionaryModelConnection";
        }

        protected override PersonDictionaryModelContext CreateNewInstance(DbContextOptions<PersonDictionaryModelContext> options)
        {
            return PersonDictionaryModelContext.CreateDesignTimeInstance(options);
        }

        protected override void Configure(SqlServerDbContextOptionsBuilder builder)
        {
            base.Configure(builder);

            builder.MigrationsHistoryTable(PersistenceBootstrap.MigrationsHistoryTableName);
        }
    }
}
