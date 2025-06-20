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
    public class clsSemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.ToTable("Semesters");
            builder.HasKey(s=>s.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x=>x.OfferedCourses).WithOne(x=>x.Semester).HasForeignKey(x=>x.SemesterId);

        }
    }
}
