using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.DTOs;


namespace Infrastructure.Repositories
{
    public class LevelReopsitoriy : GenericRepo<Level>, ILevelRepositiry
    {
        private readonly ApplicationDbContext _context;
        public LevelReopsitoriy(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    
        public async Task<IEnumerable<LevelWithSemesterDTO>> GetLevelsWithSemesterAsync(int DepartmentID)
        {
            return await _context.Levels
                .Where(x => x.DepartmentId == DepartmentID)
                .Select(x => new LevelWithSemesterDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Order = x.order,
                    DepartmentId = x.DepartmentId,
                    Semesters = x.Semesters.Select(s => new SemesterDTO
                    {
                        Id = s.Id,
                        LevelId = s.LevelId,
                        order = s.Order,
                        Name = s.Name,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<LevelWithSemesterDTO> GetLevelWithSemesterAsync(int levelId)
        {
            return await _context.Levels
                .Where(x => x.Id == levelId)
                .Select(x => new LevelWithSemesterDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Order = x.order,
                    DepartmentId = x.DepartmentId,
                    Semesters = x.Semesters.Select(s => new SemesterDTO
                    {
                        Id = s.Id,
                        LevelId = s.LevelId,
                        order=s.Order,
                        Name = s.Name,
                    }).ToList()
                }).FirstOrDefaultAsync();
        }



    }
}
