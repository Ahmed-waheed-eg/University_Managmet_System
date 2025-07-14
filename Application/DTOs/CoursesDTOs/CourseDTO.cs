using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Gredite Hour is Required.") ,Range(1, 10, ErrorMessage = "Hours must be between 1 and 10.")]
        public int Hours { get; set; }

        public string Description { get; set; }
    }
}
