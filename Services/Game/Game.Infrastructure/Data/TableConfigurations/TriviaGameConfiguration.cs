using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Infrastructure.Data.TableConfigurations
{
    internal class TriviaGameConfiguration : IEntityTypeConfiguration<TriviaGame>
    {
        public void Configure(EntityTypeBuilder<TriviaGame> builder)
        {
            builder.HasOne(x => x.CurrentGameQuestion)
                .WithMany().OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        }
    }
}
