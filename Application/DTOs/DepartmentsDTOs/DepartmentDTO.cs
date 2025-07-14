using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.DepartmentsDTOs
{
    public class DepartmentDTO
    {
        public int ID {  get; set; }
        [Required(ErrorMessage ="The name is required.")]
        [MaxLength(100,ErrorMessage ="the lenth of name should be no more than 100")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
