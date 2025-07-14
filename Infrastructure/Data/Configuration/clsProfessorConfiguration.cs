using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class clsProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {

            builder.HasKey(p => p.Id);
            builder.ToTable("Professors");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.HashPassword)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(15);
            builder.Property(p => p.DateOfHire)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(p => p.Department)
                .WithMany(d => d.Professors)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Course)
                .WithMany(c => c.Professors)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
