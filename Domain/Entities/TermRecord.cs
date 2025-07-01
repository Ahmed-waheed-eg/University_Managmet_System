using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TermRecord
    {
        public int Id { get; set; }

        public int studentId { get; set; }
        public Student Student { get; set; }


        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public bool IsCurrent { get; set; } = true;
        public bool IsCompleted { get; set; } = false;

        public double GPA { get; set; } = 0.0;
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
