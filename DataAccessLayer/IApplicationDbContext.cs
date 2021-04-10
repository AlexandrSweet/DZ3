using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IApplicationDbContext
    {
        public DbSet<Employees> Employees { get; set; }

        public DbSet<Achievements> Achievements { get; set; }
        public DbSet<HiringHistories> HiringHistories { get; set; }

    }
}
