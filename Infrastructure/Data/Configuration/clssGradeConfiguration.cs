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
    public class clssGradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EnrollmentId).IsRequired();

           // builder.HasOne<Enrollment>().WithOne(c=>c.Grade).HasForeignKey<Grade>(c=>c.EnrollmentId);

        }


  }
}
