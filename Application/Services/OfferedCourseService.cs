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
        (IOfferedCourseRepository _offeredCourseRepository,IUnitOfWork _unitOfWork,ICourseRepository _courseRepository, IStudentRepository _studentRepository
        , ISemesterRepository _semesterRepository, ILevelRepositiry _levelRepositiry,ITermRecoredRepositroy _termRecoredRepositroy)
    {


        public async Task<(bool Success, int id, string ErrorMessage)> CreateAsync(int CourseId,int SemesterID)
        {


            var exists = await _offeredCourseRepository.GetByAsync(c=>c.CourseId==CourseId&&c.SemesterId==SemesterID);
            if (exists != null)
            {
                return (false, 0, "This Offered Course already exists.");
            }
            var CourseExists= await _courseRepository.GetByAsync(c => c.Id == CourseId);
            if (CourseExists == null)
            {
                return (false, 0, "This Course does not exist.");
            }
            var Semester = await _semesterRepository.GetByAsync(s => s.Id == SemesterID);
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
                CourseId = course.Id,
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
                    CourseId = course.Id,
                    CourseName = course.Name
                });
            }
            return result;

        }

        public async Task<IEnumerable<OfferedCoursesDTO>> GetAllPerLevelAsync(int LevelID)
        {
            var offeredCourses = await _offeredCourseRepository.GetAllAsync(c => c.LevelId == LevelID);
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
                    CourseId = course.Id,
                    CourseName = course.Name
                });
            }
            return result;

        }

        public async Task<IEnumerable<OfferedCoursesDTO>> GetAllperDepartmentAsync(int DepartmentID)
        {
            var offeredCourses = await _offeredCourseRepository.GetAllAsync(c => c.DepartmentId == DepartmentID);
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
                    CourseId=course.Id,
                    CourseName = course.Name
                });
            }
            return result;

        }


        public async Task<(bool Success,IEnumerable<OfferedCoursesDTO>,string message)> GetAllPerStudentIDAsync(int StudentID)
        {

            var StudentEx = await _studentRepository.AnyAsync(s => s.Id == StudentID);
            if (!StudentEx)
            {
                return (false, null, "This Student does not exist.");
            }
            var semesterRecord= await _termRecoredRepositroy.GetByAsync(s => s.studentId == StudentID&&s.IsCurrent);
            if (semesterRecord == null)
            {
                return (false, null, "This Student does not have  semester Registeration.");
            }
            var semester= await _semesterRepository.GetByIdAsync(semesterRecord.SemesterId);
            var offeredCourses = await _offeredCourseRepository.GetAllAsync(c => c.SemesterId == semester.Id);
            if (!offeredCourses.Any())
            {
                return (true, new List<OfferedCoursesDTO>(), "No Offered Courses found for this Student.");
            }

            var result = new List<OfferedCoursesDTO>();
            foreach (var offeredCourse in offeredCourses)
            {
                var course = await _courseRepository.GetByIdAsync(offeredCourse.CourseId);
                result.Add(new OfferedCoursesDTO
                {
                    Id = offeredCourse.Id,
                    CourseId = course.Id,
                    CourseName = course.Name
                });
            }
            return (true, result, "Offered Courses retrieved successfully.");



        }

       
        public async Task<PaginationDTO<OfferedCoursesDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var pagination = await _offeredCourseRepository.GetAllAsync(pageNumber, pageSize);
            return new PaginationDTO<OfferedCoursesDTO>
            {
                values = pagination.values.Select(c => new OfferedCoursesDTO
                {
                    Id = c.Id,
                    CourseId = c.CourseId,
                    CourseName = _courseRepository.GetByIdAsync(c.CourseId).Result?.Name ?? "Unknown Course"
                }),
                TotalCount = pagination.TotalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

    }
}
