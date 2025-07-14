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
    public class EnrollmentServices
        (
        IEnrollmentRepository _enrollmentRepository,
        IOfferedCourseRepository _offeredCourseRepository,
        ITermRecoredRepositroy _termRecoredRepositroy,
        IStudentRepository _studentRepository,
        IUnitOfWork _unitOfWork
        )
    {

        public async Task<(bool Success,int EnrollmentId,string message)>EnrollStudentInCourse(int studentId,int OfferdCourseId)
        {
            var exitStudent = await _studentRepository.AnyAsync(s => s.Id == studentId);
            var OfferdCourse = await _offeredCourseRepository.GetByAsync(o => o.Id == OfferdCourseId && o.IsActive);
            if(!exitStudent)
            {
                return (false, 0, "there is no student with this id.");
            }
            if(OfferdCourse==null)
            {
                return (false, 0, "there is no offered course with this id");
            }

            var TermRecord=  await _termRecoredRepositroy.GetByAsync(t=>t.studentId==studentId&&t.IsCurrent);
            if (TermRecord == null) { return (false, 0, "this student not Enrollment in semester."); }

            if (TermRecord.SemesterId != OfferdCourse.SemesterId) { return (false, 0, "this course not offferd for this student."); }
            
            var ExistsTestOfer=await _enrollmentRepository.GetByAsync(e=>e.CourseId==OfferdCourse.CourseId&&e.TermRecordId==TermRecord.Id);
            if(ExistsTestOfer!=null)
            {
                return (false, ExistsTestOfer.Id, $"Student already Enrollment in that course id={ExistsTestOfer.Id}");
            }


            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = OfferdCourse.CourseId,
                TermRecordId = TermRecord.Id
            };

              await _enrollmentRepository.AddAsync(enrollment);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, enrollment.Id, "Enrollmet Successfully.");


            }

            return (false, 0, "erorr in save data.");


        }



    }
}
