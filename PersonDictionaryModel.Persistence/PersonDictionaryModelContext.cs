using Microsoft.EntityFrameworkCore;
using PersonDictionaryModel.Core.Domain.Models;
using System;
using System.Reflection;

namespace PersonDictionaryModel.Persistence
{
    public sealed class PersonDictionaryModelContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<RelatedPerson> RelatedPeople { get; set; }

        public PersonDictionaryModelContext(
            DbContextOptions<PersonDictionaryModelContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                t => t.Namespace.Contains("PersonDictionaryModel", StringComparison.OrdinalIgnoreCase));
        }

        public static PersonDictionaryModelContext CreateDesignTimeInstance(
           DbContextOptions<PersonDictionaryModelContext> options)
        {
            return new PersonDictionaryModelContext(options);
        }
    }
}
