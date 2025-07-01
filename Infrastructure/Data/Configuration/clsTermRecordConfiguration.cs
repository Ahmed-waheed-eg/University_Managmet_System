using Microsoft.EntityFrameworkCore;
using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class clsTermRecordConfiguration : IEntityTypeConfiguration<TermRecord>
    {
        public void Configure(EntityTypeBuilder<TermRecord> builder)
        {

            builder.HasKey(tr => tr.Id);
            builder.ToTable("TermRecords");
            builder.Property(tr => tr.IsCurrent)
                .HasDefaultValue(true);
            builder.Property(tr => tr.IsCompleted)
                .HasDefaultValue(false);
            builder.Property(tr => tr.GPA)
                .HasDefaultValue(0.0);

            builder.HasOne(tr => tr.Student)
                .WithMany(s => s.TermRecords)
                .HasForeignKey(tr => tr.studentId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(tr => tr.Semester)
                .WithMany(s => s.TermRecords)
                .HasForeignKey(tr => tr.SemesterId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(tr => tr.Enrollments)
                .WithOne(e => e.TermRecord)
                .HasForeignKey(e => e.TermRecordId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
