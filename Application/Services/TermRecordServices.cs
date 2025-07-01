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
    public class TermRecordServices
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
                return await CreateFirstTime(student);
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
                    return await CreateNextTermRecordForTerm1(student, CurrentSemesterActive,LastTermRecord);
                    break;
                case 2:
                    return await CreateNextTermRecordForTerm2(student, CurrentSemesterActive, LastTermRecord);
                    break;
                case 3:
                    return await CreateNextTermRecordForTerm3(student, CurrentSemesterActive, LastTermRecord);
                    break;
                default:
                    return (false, null, "Invalid semester order.");
                    break;
            }

        }



        private async Task<(bool Success, TermRecordDTO DTO, string message)> CreateFirstTime(Student student)
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
        private async Task<(bool Success, TermRecordDTO DTO, string message)> CreateNextTermRecordForTerm2(Student student,Semester CurrentSemester,TermRecord LastTermRecord)
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
        private async Task<(bool Success, TermRecordDTO DTO, string message)> CreateNextTermRecordForTerm3(Student student, Semester CurrentSemester, TermRecord LastTermRecord)
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

        private async Task<(bool Success, TermRecordDTO DTO, string message)> CreateNextTermRecordForTerm1(Student student, Semester CurrentSemester,TermRecord LastTermRecord)
        {

            var Levels = await _levelRepositiry.GetAllAsync(l => l.DepartmentId == student.DepartmentId);
            var LastLevel= await _levelRepositiry.GetByIdAsync(CurrentSemester.LevelId);
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
