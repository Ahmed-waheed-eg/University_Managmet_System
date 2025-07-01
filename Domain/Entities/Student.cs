using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int NationalId { get; set; }

        public StudentStatus Status { get; set; } = StudentStatus.Active; // Default status is Active

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    

        
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<TermRecord> TermRecords { get; set; }


    }
}
