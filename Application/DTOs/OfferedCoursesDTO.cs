﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OfferedCoursesDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
