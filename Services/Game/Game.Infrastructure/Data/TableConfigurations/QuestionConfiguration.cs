using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Infrastructure.Data.TableConfigurations;

internal class QuestionConfiguration: IEntityTypeConfiguration<Question>
{
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasMany(x => x.Answers)
            .WithOne()
            .HasForeignKey("QuestionId").IsRequired();
        }
}