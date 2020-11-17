using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeSheetApp.Data;
using TimeSheetApp.Models;

namespace TimeSheetApp.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly TimeSheetApp.Data.TimeSheetAppContext _context;

        public IndexModel(TimeSheetApp.Data.TimeSheetAppContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string EmployeeFiles { get; set; }

        [BindProperty(SupportsGet = true)]


        public string fromdate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string todate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string totalhours { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Employee
                                            orderby m.LastName
                                            select m.LastName;
            var employees = from m in _context.Employee
                         select m;
            var sum1 = _context.Employee.Sum(p => p.Hours);

            totalhours = sum1.ToString();
            if (!string.IsNullOrEmpty(SearchString))
            {
                
                employees = employees.Where(s => s.FirstName.Contains(SearchString));

                var sum3 = employees.Where(s => s.FirstName.Contains(SearchString)).Sum(a => a.Hours);

                totalhours = sum3.ToString();
            }

            if (!string.IsNullOrEmpty(fromdate))
            {
                if (string.IsNullOrWhiteSpace(todate))
                {
                    todate = DateTime.Today.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrWhiteSpace(fromdate))
                {
                    employees = employees.Where(s => s.Date >= Convert.ToDateTime(fromdate) && s.Date <= Convert.ToDateTime(todate));

                    var sum2 = employees.Where(s => s.Date >= Convert.ToDateTime(fromdate) && s.Date <= Convert.ToDateTime(todate)).Sum(a=>a.Hours);

                    totalhours = sum2.ToString();

                }
            }

            if (!string.IsNullOrEmpty(EmployeeFiles))
            {
                employees = employees.Where(x => x.LastName == EmployeeFiles);

                var sum4= employees.Where(x => x.LastName == EmployeeFiles).Sum(a=> a.Hours);
                totalhours = sum4.ToString();
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Employee = await employees.ToListAsync();
        }
    }
}
