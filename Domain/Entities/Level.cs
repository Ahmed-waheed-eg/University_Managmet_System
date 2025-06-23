using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
      //  public int NumberLevel { get; set; }
         public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Semester> Semesters { get; set; }
        public ICollection<OfferedCourse> OfferedCourses { get; set; }

    }
}
