using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Employees
{
    public class EmployeeAttendanceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ILogger<HomeController> _logger;
        //private EmployeeDBContext Context { get; }
        private ApplicationDBContext Context { get; }
        public List<string> AllDaysOfWeekList = new List<string>();



        public EmployeeAttendanceController(ApplicationDBContext _context, UserManager<ApplicationUser> userManager)
        {
            this.Context = _context;
            _userManager = userManager;

        }

        public IActionResult Index(string SelectedMonth, string SelectedYear)
        {
            int mon, year, noOfDays = 0;

            if (SelectedMonth == null || SelectedYear == null)
            {
                SelectedMonth = DateTime.Now.ToString("MM");
                SelectedYear = DateTime.Now.ToString("yyyy");
            }



            mon = int.Parse(SelectedMonth);
            year = int.Parse(SelectedYear);

            noOfDays = TotalNumberOfDaysInMonth(year, mon);
            AllDaysOfWeekList = GetDaysOfWeek(year, mon, noOfDays);
            ViewBag.data= AllDaysOfWeekList;

            var employeesAttendances = (from pd in Context.Attendance
                                        join od in Context.Employee on pd.Id equals od.Id
                                        where pd.CheckInTime.Value.Month == mon
                             && pd.CheckInTime.Value.Year == year
                                        select new EmployeesAttendanceReportViewModel
                                        {
                                            EmployeeList = od,
                                            AttendanceList = pd,
                                        }).ToList();


            ViewData["EmployeeList"] = new SelectList(Context.Employee, "Id", "FirstName");

            return View(employeesAttendances);
        }

        [HttpPost]
        public IActionResult MarkAttendance(Attendance attendance)
        {
            attendance.CreatedOn = DateTime.Now;
            this.Context.Attendance.Add(attendance);
            this.Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public int TotalNumberOfDaysInMonth(int year, int month)
        {
           
            

            return DateTime.DaysInMonth(year, month);
        }

        public List<string> GetDaysOfWeek(int year, int month,int noOfDays)
        {
            List<string> DaysOfWeekList = new List<string>();

            for(int i = 1; i <= noOfDays; i++)
            {
                DateTime dateValue = new DateTime(year, month, i);
                DaysOfWeekList.Add(dateValue.ToString("ddd"));
            }
           

            return DaysOfWeekList;
        }



    }
}
