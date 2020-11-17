using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheetApp.Models;

namespace TimeSheetApp.Data
{
    public class TimeSheetAppContext : DbContext
    {
        public TimeSheetAppContext (DbContextOptions<TimeSheetAppContext> options)
            : base(options)
        {
        }

        public DbSet<TimeSheetApp.Models.Employee> Employee { get; set; }
    }
}
