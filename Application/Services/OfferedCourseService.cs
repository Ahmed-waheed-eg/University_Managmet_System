using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OfferedCousreServices
        (IOfferedCourseRepository _offeredCourseRepository,IUnitOfWork _unitOfWork,ICourseRepository _courseRepository
        ,ISemesterRepository _semesterRepository, ILevelRepositiry _levelRepositiry,IDepartmentRepositiry _departmentRepositiry)
    {


        public async Task<(bool Success, int id, string ErrorMessage)> CreateAsync(int CourseId,int SemesterID)
        {


            var exists = await _offeredCourseRepository.GetByAsync(c=>c.CourseId==CourseId&&c.SemesterId==SemesterID);
            if (exists != null)
            {
                return (false, 0, "This Offered Course already exists.");
            }
            var CourseExists= await _courseRepository.GetByExpesAsync(c => c.Id == CourseId);
            if (CourseExists == null)
            {
                return (false, 0, "This Course does not exist.");
            }
            var Semester = await _semesterRepository.GetByExpesAsync(s => s.Id == SemesterID);
            if (Semester == null)
            {
                return (false, 0, "This Semester does not exist.");
            }
            var Level = await _levelRepositiry.GetByIdAsync(Semester.LevelId);


            var offeredCourse = new OfferedCourse { CourseId=CourseId,SemesterId=SemesterID,LevelId=Level.Id,DepartmentId=Level.DepartmentId };
           
            await _offeredCourseRepository.AddAsync(offeredCourse);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, offeredCourse.Id, "Created Successfully");
            }
            return (false, 0, "Error in saving changes");
        }

        public async Task<OfferedCoursesDTO> GetByIdAsync(int Id)
        {
            var offeredCourse = await _offeredCourseRepository.GetByIdAsync(Id);
            if (offeredCourse == null)
            {
                return null;
            }
            var course = await _courseRepository.GetByIdAsync(offeredCourse.CourseId);
            return new OfferedCoursesDTO
            {
                Id = offeredCourse.Id,
                CourseName = course.Name
            };
        }
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int Id)
        {
            var offeredCourse = await _offeredCourseRepository.GetByIdAsync(Id);
            if (offeredCourse == null)
            {
                return (false, "This ID not found.");
            }
            _offeredCourseRepository.Delete(offeredCourse);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Deleted Successfully.");
            }
            return (false, "Error in saving changes");
        }

        public async Task<IEnumerable<OfferedCoursesDTO>> GetAllPerSemesterAsync(int SemesterID)
        {
            var offeredCourses = await _offeredCourseRepository.GetAllAsync(c => c.SemesterId == SemesterID);
            if (!offeredCourses.Any())
            {
                return new List<OfferedCoursesDTO>();
            }
            var result = new List<OfferedCoursesDTO>();
           
            foreach (var offeredCourse in offeredCourses)
            {
                var course = await _courseRepository.GetByIdAsync(offeredCourse.CourseId);
             
                result.Add(new OfferedCoursesDTO
                {
                    Id = offeredCourse.Id,
                    CourseName = course.Name
                });
            }
            return result;

        }

        public async Task<IEnumerable<OfferedCoursesDTO>> GetAllPerLevelAsync(int LevelID)
        {
            var offeredCourses = await _offeredCourseRepository.GetAllAsync(c => c.SemesterId == LevelID);
            if (! offeredCourses.Any())
            {
                return new List<OfferedCoursesDTO>();
            }
            var result = new List<OfferedCoursesDTO>();
            foreach (var offeredCourse in offeredCourses)
            {
                var course = await _courseRepository.GetByIdAsync(offeredCourse.CourseId);

                result.Add(new OfferedCoursesDTO
                {
                    Id = offeredCourse.Id,
                    CourseName = course.Name
                });
            }
            return result;

        }

        public async Task<IEnumerable<OfferedCoursesDTO>> GetAllDepartmentLevelAsync(int DepartmentID)
        {
            var offeredCourses = await _offeredCourseRepository.GetAllAsync(c => c.SemesterId == DepartmentID);
            if (! offeredCourses.Any())
            {
                return new List<OfferedCoursesDTO>();
            }
            var result = new List<OfferedCoursesDTO>();
            foreach (var offeredCourse in offeredCourses)
            {
                var course = await _courseRepository.GetByIdAsync(offeredCourse.CourseId);

                result.Add(new OfferedCoursesDTO
                {
                    Id = offeredCourse.Id,
                    CourseName = course.Name
                });
            }
            return result;

        }

    }
}
