using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Infrastructure.Data.TableConfigurations;

public class GameQuestionConfiguration : IEntityTypeConfiguration<GameQuestion>
{
    public void Configure(EntityTypeBuilder<GameQuestion> builder)
    {
        builder.HasOne(g => g.NextGameQuestion)
            .WithMany()
            .HasPrincipalKey("Id")
            .HasForeignKey("NextGameQuestionId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}