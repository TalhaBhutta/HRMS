using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var cId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Customer c = Context.Customer.FirstOrDefault(u => u.UserId == cId);

            int mon, year, noOfDays = 0;

            if (SelectedMonth == null || SelectedYear == null)
            {
                SelectedMonth = DateTime.Now.ToString("MM");
                SelectedYear = DateTime.Now.ToString("yyyy");
            }

            

            mon = int.Parse(SelectedMonth);
            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = new DateTimeOffset(2008, mon, 1, 8, 6, 32,
                                 new TimeSpan(1, 0, 0));

            year = int.Parse(SelectedYear);

            noOfDays = TotalNumberOfDaysInMonth(year, mon);
            AllDaysOfWeekList = GetDaysOfWeek(year, mon, noOfDays);
            ViewBag.data= AllDaysOfWeekList;

            //var results = (from a in Context.Attendance
            //               join b in Context.Assignment on a.AssignmentId equals b.Id
            //               join s in Context.Shift on b.ShiftId equals s.Id
            //               join cb in Context.CustomerBranch on s.BranchId equals cb.BranchId
            //               join e in Context.Employee on a.EmployeeId equals e.Id
            //               where cb.CustomerId == c.Id
            //                 && a.CheckOutTime.Value.Month == mon
            //                 && a.CheckInTime.Value.Year == year
            //               select new CustomerAttendanceReportViewModel()
            //               {
            //                   Emp_name = e.FirstName + ' ' + e.LastName,
            //                   empAttendances = Context.Attendance.Where(a => a.EmployeeId == e.Id).ToList()
            //               }).ToList();

            var employeesAttendances = (from pd in Context.Attendance
                                         join od in Context.Employee on pd.EmployeeId equals od.Id
                                        where pd.Month == SelectedMonth
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
