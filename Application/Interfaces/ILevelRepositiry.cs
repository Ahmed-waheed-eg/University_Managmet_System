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
   
        Task<IEnumerable<LevelWithSemesterDTO>> GetLevelsWithSemesterAsync(int DepartmentID);
        Task<LevelWithSemesterDTO> GetLevelWithSemesterAsync(int levelId);

        
    }
}
