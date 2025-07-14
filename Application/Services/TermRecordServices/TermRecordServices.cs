using System;
using System.Collections.Generic;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public partial class TermRecordServices
        (
             ISemesterRepository _semesterRepository,
             ILevelRepositiry _levelRepositiry,
             ITermRecoredRepositroy _termRecoredRepositroy,
             IStudentRepository _studentRepository,
             IDepartmentRepositiry _departmentRepositiry,
             IUnitOfWork _unitOfWork
        )
    {
        public async Task<TermRecordDTO> GetTermRecordAsync(int id)
        {
            var termRecord = await _termRecoredRepositroy.GetByIdAsync(id);
            if (termRecord == null)
            {
                return null;
            }
            return new TermRecordDTO
            {
                Id = termRecord.Id,
                SemesterId = termRecord.SemesterId,
                StudentId = termRecord.studentId,
                IsCurrent = termRecord.IsCurrent,
                IsCompleted = termRecord.IsCompleted,
                GPA = termRecord.GPA
            };
        }

        public async Task<TermRecordDTO> GetTermRecordByStudentIdAsync(int StudentID)
        {
            var termRecord = await _termRecoredRepositroy.GetByAsync(t=>t.studentId==StudentID&&t.IsCurrent);
            if (termRecord == null)
            {
                return null;
            }
            return new TermRecordDTO
            {
                Id = termRecord.Id,
                SemesterId = termRecord.SemesterId,
                StudentId = termRecord.studentId,
                IsCurrent = termRecord.IsCurrent,
                IsCompleted = termRecord.IsCompleted,
                GPA = termRecord.GPA
            };
        }


        public async Task<(bool Success,TermRecordDTO DTO,string message)>AssignsStudentInSemester(int StudetID)
        {
            var student = await _studentRepository.GetByIdAsync(StudetID);
            if (student == null)
            {
                return (false, null, "Student not found");
            }
            var isExists = await _termRecoredRepositroy.AnyAsync(t => t.studentId == StudetID );
            if (!isExists)
            {
                return await _CreateFirstTime(student);
            }

            var LastTermRecord = await _termRecoredRepositroy.GetByAsync(t => t.studentId == StudetID && t.IsCurrent);
            var semes = await _semesterRepository.GetByIdAsync(LastTermRecord.SemesterId);
            if (semes.IsActive)
            {
                return (false, null, "This Student is already assigned in Active Semester.");
            }

            
            var CurrentSemesterActive = await _semesterRepository.GetByAsync(s => s.IsActive && s.LevelId == LastTermRecord.Semester.LevelId);
            switch (CurrentSemesterActive.Order)
            {
                case 1:
                    return await _CreateNextTermRecordForTerm1(student, CurrentSemesterActive,LastTermRecord);
                    break;
                case 2:
                    return await _CreateNextTermRecordForTerm2(student, CurrentSemesterActive, LastTermRecord);
                    break;
                case 3:
                    return await _CreateNextTermRecordForTerm3(student, CurrentSemesterActive, LastTermRecord);
                    break;
                default:
                    return (false, null, "Invalid semester order.");
                    break;
            }
           

        }

        //public async Task <(bool Success, TermRecordDTO DTO,string message)>GetAllByUserId






    }
}
