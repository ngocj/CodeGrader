using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class UserProgressConfig : IEntityTypeConfiguration<UserProgress>
    {
        public UserProgressConfig()
        {
        }

        public void Configure(EntityTypeBuilder<UserProgress> builder)
        {
            builder.ToTable(nameof(UserProgress));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
