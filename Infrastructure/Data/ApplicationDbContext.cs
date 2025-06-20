using Domain.Entities;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Semester> Semesters { get; set; } 
        public DbSet<Course> Courses { get; set; }
        public DbSet<OfferedCourse> OfferedCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new clsDepartmentConfiguration ()); 
            modelBuilder.ApplyConfiguration(new clsLevelConfiguration ());
            modelBuilder.ApplyConfiguration(new clsSemesterConfiguration ());
            modelBuilder.ApplyConfiguration (new clsCourseConfiguration ());
            modelBuilder.ApplyConfiguration(new clsOfferedCourseConfiguration());

        }



    }
}
