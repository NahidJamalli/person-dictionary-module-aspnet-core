using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDictionaryModel.Core.Domain.Enums;
using PersonDictionaryModel.Core.Domain.Models;

namespace PersonDictionaryModel.Persistence.Configurations
{
    public sealed class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RelativeType);

            builder.HasOne(x => x.Person)
                .WithMany(x => x.RelatedPeople);


            builder.HasData(
                new RelatedPerson()
                {
                    Id = 1,
                    PersonId = 1,
                    RelativeType = RelativeType.CoWorker
                },
                new RelatedPerson()
                {
                    Id = 2,
                    PersonId = 1,
                    RelativeType = RelativeType.Relative
                },
                new RelatedPerson()
                {
                    Id = 3,
                    PersonId = 2,
                    RelativeType = RelativeType.Other
                },
                new RelatedPerson()
                {
                    Id = 4,
                    PersonId = 3,
                    RelativeType = RelativeType.Relative
                });
        }
    }
}
