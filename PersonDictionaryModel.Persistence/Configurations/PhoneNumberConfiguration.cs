using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDictionaryModel.Core.Domain.Models;

namespace PersonDictionaryModel.Persistence.Configurations
{
    public sealed class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).HasMaxLength(50);

            builder.HasOne(x => x.Person)
                .WithMany(x => x.PhoneNumbers);

            builder.HasData(
                new PhoneNumber()
                {
                    Id = 1,
                    Number = "055",
                    PersonId = 1
                },
                new PhoneNumber()
                {
                    Id = 2,
                    Number = "051",
                    PersonId = 1
                },
                new PhoneNumber()
                {
                    Id = 3,
                    Number = "077",
                    PersonId = 2
                },
                 new PhoneNumber()
                 {
                     Id = 4,
                     Number = "050",
                     PersonId = 3
                 });
        }
    }
}
