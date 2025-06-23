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
    public class clsDepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
       

public void Configure(EntityTypeBuilder<Department> builder)
        {


            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Description).HasMaxLength(300);
            builder.HasMany(x=>x.Levels)
                .WithOne(x => x.Department).HasForeignKey(x=>x.DepartmentId);

            builder.HasMany(x => x.OfferedCourses).WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId);

        }
    }
}
