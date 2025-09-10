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
    public class ProblemStatsConfig : IEntityTypeConfiguration<ProblemStats>
    {
        public ProblemStatsConfig()
        {
        }

        public void Configure(EntityTypeBuilder<ProblemStats> builder)
        {
            builder.ToTable(nameof(ProblemStats));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
