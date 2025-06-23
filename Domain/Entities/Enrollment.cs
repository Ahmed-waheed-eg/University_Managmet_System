using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Enrollment
    {public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int OfferedCourseId { get; set; }
        public OfferedCourse OfferedCourse { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public string Grade { get; set; } // e.g., "A", "B", "C", etc.

    }
}
