using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; } 
        public string PhoneNumber { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } 
        public DateTime DateOfHire { get; set; } = DateTime.UtcNow;
        
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
