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
            builder.Property(x=>x.DepartmentId).IsRequired();
            builder.HasOne(x => x.Department).WithMany(o => o.OfferedCourses).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Level).WithMany(o => o.OfferedCourses).HasForeignKey(x => x.LevelId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Course).WithMany(o=>o.OfferedCourses).HasForeignKey(x=>x.CourseId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Semester).WithMany(o => o.OfferedCourses).HasForeignKey(x => x.SemesterId).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
