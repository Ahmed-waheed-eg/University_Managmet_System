using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ILevelRepositiry : IRepository<Level>
    {
        Task<IEnumerable<Level>> GetDepartmentsWithLevelAsync(int DepartmentID);
        Task<IEnumerable<LevelWithSemesterDTO>> GetLevelWithSemesterAsync();
        Task<LevelWithSemesterDTO> GetLevelWithSemesterAsync(int levelId);

        Task<IEnumerable<SemesterDTO>> GetSemestersByLevelIdAsync(int levelId);
        Task<bool> IsLevelNameExistsInDepartment(string levelName,int departmentId);
    }
}
