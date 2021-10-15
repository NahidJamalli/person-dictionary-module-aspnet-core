using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Domain.Models;
using System;

namespace PersonDictionaryModel.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.PersonalNumber).HasMaxLength(11);
            builder.Property(x => x.Gender);
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.TargetUrl);

            builder.HasMany(x => x.PhoneNumbers)
                .WithOne(x => x.Person);

            builder.HasOne(x => x.City)
                .WithMany(x => x.People);

            builder.HasMany(x => x.RelatedPeople)
                .WithOne(x => x.Person);

            builder.HasData(
                new Person()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = DateTime.Now.AddYears(-21),
                    Gender = Gender.Male,
                    PersonalNumber = "xU7u4nqmeAN",
                    CityId = 1,
                    TargetUrl = null,
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "Ella",
                    LastName = "Doe",
                    BirthDate = DateTime.Now.AddYears(-15),
                    Gender = Gender.Female,
                    PersonalNumber = "6TZ5UwQw5Rt",
                    CityId = 2,
                    TargetUrl = null,
                },
                new Person()
                {
                    Id = 3,
                    FirstName = "Marta",
                    LastName = "Bella",
                    BirthDate = DateTime.Now.AddYears(-32),
                    Gender = Gender.Female,
                    PersonalNumber = "cpNb9xJNR8g",
                    CityId = 3,
                    TargetUrl = null,
                });
        }
    }
}
