using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OfferedCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int LevelId { get; set; }
        public Level Level { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public bool IsActive { get; set; }=true;

        public ICollection<Enrollment> Enrollments { get; set; } 

    }
}
