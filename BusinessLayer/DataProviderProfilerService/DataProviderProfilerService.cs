using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BusinessLayer.DataProviderProfilerService
{
    public class DataProviderProfilerService : IDataProviderProfilerService
    {
        private readonly IApplicationDbContext _dbContext;
        public DataProviderProfilerService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Dictionary<string, string> ComparePerformance ()
        {
            var leadTime = new Dictionary<string, string>();
            var LinqMethodLeadTime = new Stopwatch();
            var RawSqlQueryLeadTime = new Stopwatch();            

            RawSqlQueryLeadTime.Start();
            var resaultSqlMethod = _dbContext.Employees.FromSqlRaw("SELECT * FROM Employees").ToList();
            RawSqlQueryLeadTime.Stop();
            leadTime.Add("RawSqlQueryLeadTime", RawSqlQueryLeadTime.ElapsedMilliseconds.ToString());

            LinqMethodLeadTime.Start();
            var resaultLinqMethod = _dbContext.Employees.Include(e => e.HiringHistories).ThenInclude(a => a.Achievements).ToListAsync();
            LinqMethodLeadTime.Stop();
            leadTime.Add("LinqMethodLeadTime", LinqMethodLeadTime.ElapsedMilliseconds.ToString());

            return leadTime;
        }
    }
}
