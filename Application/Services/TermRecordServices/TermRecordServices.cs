using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public partial class TermRecordServices
    {


        private async Task<(bool Success, TermRecordDTO DTO, string message)> _CreateFirstTime(Student student)
        {
            var level = await _levelRepositiry.GetByAsync(l => l.DepartmentId == student.DepartmentId && l.order == 1);
            if (level == null)
            {
                return (false, null, "Level not found for the student's department.");
            }
            var semester = await _semesterRepository.GetByAsync(s => s.LevelId == level.Id && s.Order == 1 && s.IsActive);
            if (semester == null)
            {
                return (false, null, "Semester not found for the student's level or it's not Active Semester.");
            }
            var termRecord = new TermRecord
            {
                SemesterId = semester.Id,
                studentId = student.Id,
                IsCurrent = true,
                IsCompleted = false,
                GPA = 0.0
            };

            await _termRecoredRepositroy.AddAsync(termRecord);

            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, new TermRecordDTO
                {
                    Id = termRecord.Id,
                    SemesterId = termRecord.SemesterId,
                    StudentId = termRecord.studentId,
                    IsCurrent = termRecord.IsCurrent,
                    IsCompleted = termRecord.IsCompleted,
                    GPA = termRecord.GPA
                }, "Term record created successfully.");
            }
            return (false, null, "Failed to create or save term record.");
        }
        private async Task<(bool Success, TermRecordDTO DTO, string message)> _CreateNextTermRecordForTerm2(Student student, Semester CurrentSemester, TermRecord LastTermRecord)
        {
            var TermRecord = new TermRecord
            {
                SemesterId = CurrentSemester.Id,
                studentId = student.Id,
                IsCurrent = true,
                IsCompleted = false,
                GPA = 0.0
            };

            await _termRecoredRepositroy.AddAsync(TermRecord);
            LastTermRecord.IsCurrent = false;
            _termRecoredRepositroy.Update(LastTermRecord);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, new TermRecordDTO
                {
                    Id = TermRecord.Id,
                    SemesterId = TermRecord.SemesterId,
                    StudentId = TermRecord.studentId,
                    IsCurrent = TermRecord.IsCurrent,
                    IsCompleted = TermRecord.IsCompleted,
                    GPA = TermRecord.GPA
                }, "Term record created successfully.");
            }
            return (false, null, "Failed to create or save term record.");


        }
        private async Task<(bool Success, TermRecordDTO DTO, string message)> _CreateNextTermRecordForTerm3(Student student, Semester CurrentSemester, TermRecord LastTermRecord)
        {
            var TermRecord = new TermRecord
            {
                SemesterId = CurrentSemester.Id,
                studentId = student.Id,
                IsCurrent = true,
                IsCompleted = false,
                GPA = 0.0
            };
            await _termRecoredRepositroy.AddAsync(TermRecord);
            LastTermRecord.IsCurrent = false;
            _termRecoredRepositroy.Update(LastTermRecord);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, new TermRecordDTO
                {
                    Id = TermRecord.Id,
                    SemesterId = TermRecord.SemesterId,
                    StudentId = TermRecord.studentId,
                    IsCurrent = TermRecord.IsCurrent,
                    IsCompleted = TermRecord.IsCompleted,
                    GPA = TermRecord.GPA
                }, "Term record created successfully.");
            }
            return (false, null, "Failed to create or save term record.");
        }

        private async Task<(bool Success, TermRecordDTO DTO, string message)> _CreateNextTermRecordForTerm1(Student student, Semester CurrentSemester, TermRecord LastTermRecord)
        {

            var Levels = await _levelRepositiry.GetAllAsync(l => l.DepartmentId == student.DepartmentId);
            var LastLevel = await _levelRepositiry.GetByIdAsync(CurrentSemester.LevelId);
            if (LastLevel.order == Levels.Count())
            {
                return (false, null, "This Student is already in the last level of the department.and he gradution");
            }
            var NextLevel = Levels.FirstOrDefault(l => l.order == LastLevel.order + 1);
            var semester = await _semesterRepository.GetByAsync(s => s.LevelId == NextLevel.Id && s.Order == 1 && s.IsActive);

            var TermRecord = new TermRecord
            {
                SemesterId = semester.Id,
                studentId = student.Id,
                IsCurrent = true,
                IsCompleted = false,
                GPA = 0.0
            };
            await _termRecoredRepositroy.AddAsync(TermRecord);
            LastTermRecord.IsCurrent = false;
            _termRecoredRepositroy.Update(LastTermRecord);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, new TermRecordDTO
                {
                    Id = TermRecord.Id,
                    SemesterId = TermRecord.SemesterId,
                    StudentId = TermRecord.studentId,
                    IsCurrent = TermRecord.IsCurrent,
                    IsCompleted = TermRecord.IsCompleted,
                    GPA = TermRecord.GPA
                }, "Term record created successfully.");
            }

            return (false, null, "Failed to create or save term record.");
        }



    }
}
