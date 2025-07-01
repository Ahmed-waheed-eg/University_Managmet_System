using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Level> Levels { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<OfferedCourse> OfferedCourses { get; set; }
 
    }
}
