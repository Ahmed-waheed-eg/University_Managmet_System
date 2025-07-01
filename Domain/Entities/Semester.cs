using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int Order { get; set; } 

        public int LevelId { get; set; }
        public Level Level { get; set; }

        public bool IsActive { get; set; } = false;

        public ICollection<TermRecord> TermRecords { get; set; }
        public ICollection<OfferedCourse> OfferedCourses { get; set; }
    }
}
