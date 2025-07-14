using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EnrollmentDetailsDTO
    {
        public int EnrollmentId {  get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }

        public DateTime EnrolledAt { get; set; } 
        public bool IsPassed { get; set; } = false;
        public double Grade { get; set; } = 0.0;
        public char GPA { get; set; }

    }
}
