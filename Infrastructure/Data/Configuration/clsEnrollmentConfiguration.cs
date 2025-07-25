﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class clsEnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Enrollments");

            builder.HasOne(e=>e.Student).WithMany(s=>s.Enrollments).HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Course).WithMany(o => o.Enrollments).HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
