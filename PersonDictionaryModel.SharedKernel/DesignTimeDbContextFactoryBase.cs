namespace PersonDictionaryModel.SharedKernel
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;

    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected string ConnectionStringName { get; set; }

        public TContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            var connectionString = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build().GetSection("ConnectionStrings:PersonDictionaryModelConnection").Value;

            optionsBuilder.UseSqlServer(connectionString, Configure);

            return CreateNewInstance(optionsBuilder.Options);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        protected virtual void Configure(SqlServerDbContextOptionsBuilder builder)
        {
            builder.UseNetTopologySuite();
        }
    }
}