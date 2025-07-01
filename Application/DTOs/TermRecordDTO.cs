using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TermRecordDTO
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int StudentId { get; set; }
        public bool IsCurrent { get; set; } = true;
        public bool IsCompleted { get; set; } = false;

        public double GPA { get; set; }

    }
}
