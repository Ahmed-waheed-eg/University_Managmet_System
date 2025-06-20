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
    internal class clsOfferedCourseConfiguration : IEntityTypeConfiguration<OfferedCourse>
    {
        public void Configure(EntityTypeBuilder<OfferedCourse> builder)
        {
            builder.ToTable("OfferedCourses");
            builder.HasKey(x=>x.Id);

            builder.HasOne(x => x.Course).WithMany(o=>o.OfferedCourses).HasForeignKey(x=>x.CourseId);  
        }
    }
}
