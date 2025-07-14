using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public double FinalExamGrad { get; set; } = 0.0;
        public double QuizGrad { get; set; } = 0.0;
        public double MidTermGrad { get; set; } = 0.0;
        public double TotalGrads { get; set; } = 0.0;

        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}
