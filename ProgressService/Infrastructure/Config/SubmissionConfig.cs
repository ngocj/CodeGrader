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
    public class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public SubmissionConfig()
        {
        }

        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable(nameof(Submission));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.EvaluationCriteria, evaluationBuilder =>
            {
                evaluationBuilder.Property(e => e.Algorithm)
                    .HasColumnName("Algorithm");

                evaluationBuilder.Property(e => e.CleanCode)
                    .HasColumnName("CleanCode");
            });
        }
    }
}
