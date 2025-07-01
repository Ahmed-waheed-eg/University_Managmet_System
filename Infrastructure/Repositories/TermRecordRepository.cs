using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TermRecordRepository(ApplicationDbContext _dbContext) : GenericRepo<TermRecord>(_dbContext),ITermRecoredRepositroy
    {
    }
}
