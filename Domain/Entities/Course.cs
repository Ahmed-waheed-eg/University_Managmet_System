using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Hours { get; set; }

        public string Description { get; set; }


        public ICollection<OfferedCourse> OfferedCourses { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
