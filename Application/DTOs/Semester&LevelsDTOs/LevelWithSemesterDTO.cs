using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LevelWithSemesterDTO
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public List<SemesterDTO> Semesters { get; set; }

    }
}
