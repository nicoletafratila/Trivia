using System;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Infrastructure.Data.TableConfigurations
{
    public class AnswerConfiguration: IEntityTypeConfiguration<Answer>
    {

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();

        }
    }
}

