using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int TermRecordId { get; set; }
        public TermRecord TermRecord { get; set; }


        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public bool IsPassed { get; set; } = false;
  
        
        public char GPA { get; set; } 

        public Grade Grade { get; set; }
    }
}
