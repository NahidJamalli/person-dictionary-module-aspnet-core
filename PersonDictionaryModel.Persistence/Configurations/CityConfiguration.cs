using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDictionaryModel.Core.Domain.Models;

namespace PersonDictionaryModel.Persistence.Configurations
{
    public sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(50);

            builder.HasMany(x => x.People)
                .WithOne(x => x.City);

            builder.HasData(
                new City()
                {
                    Id = 1,
                    Name = "London",
                },
                new City()
                {
                    Id = 2,
                    Name = "Budapest"
                },
                 new City()
                 {
                     Id = 3,
                     Name = "California"
                 });
        }
    }
}