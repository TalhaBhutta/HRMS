using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using HRMS.ViewModels.Employees;
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

        

        public EmployeeAttendanceController(ApplicationDBContext _context, UserManager<ApplicationUser> userManager)
        {
            this.Context = _context;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            //var q = (from pd in Context.Attendance
            //         join od in Context.Employee on pd.Id equals od.Id
            //         orderby od.Id
            //         select new
            //         {
            //             Employee = od,
            //             Id = pd.Id,
            //             EmployeeId = pd.EmployeeId,
            //             DepartmentID = pd.DepartmentID,
            //             MarkAttendanceBY = pd.MarkAttendanceBY,
            //             Year = pd.Year,
            //             Month = pd.Month,
            //             IsLate = pd.IsLate,
            //             IsHalfDay = pd.IsHalfDay,
            //             CheckInTime = pd.CheckInTime,
            //             CheckOutTime = pd.CheckOutTime,
            //             AssignmentId = pd.AssignmentId,
            //             TimesheetId = pd.TimesheetId,
            //             ManualCheckInLat = pd.ManualCheckInLat,
            //             ManualCheckInLong = pd.ManualCheckInLong,
            //             ManualCheckOutLat = pd.ManualCheckOutLat,
            //             ManualCheckOutLong = pd.ManualCheckOutLong,
            //             ManualOutOfRangeCheckIn = pd.ManualOutOfRangeCheckIn,
            //             ManualOutOfRangeCheckOut = pd.ManualOutOfRangeCheckOut,
            //             CheckInLat = pd.CheckInLat,
            //             CheckInLong = pd.CheckInLong,
            //             CheckOutLat = pd.CheckOutLat,
            //             CheckOutLong = pd.CheckOutLong,
            //             OutOfRangeCheckIn = pd.OutOfRangeCheckIn,
            //             OutOfRangeCheckOut = pd.OutOfRangeCheckOut,
            //             NoShow = pd.NoShow,
            //             InvoiceId = pd.InvoiceId,
            //             CreatedOn = pd.CreatedOn,
            //             CreatedBy = pd.CreatedBy,
            //             ModifiedOn = pd.ModifiedOn,
            //             ModifiedBy = pd.ModifiedBy
            //         }).ToList();

            var employeesAttendances = (from pd in Context.Attendance
                     join od in Context.Employee on pd.Id equals od.Id
                     select new EmployeesAttendanceReportViewModel
                     {
                         EmployeeList = od,
                         AttendanceList = pd,
                     }).ToList();


            //var orders = from s in Context.Attendance

            //             select s;


            //    orders = orders.Where(s => s..ToUpper().Contains(searchString.ToUpper())
            //                           || s.Email.ToUpper().Contains(searchString.ToUpper())
            //        || s.Orderstatus.ToUpper().Contains(searchString.ToUpper()));



            //var results = (from a in Context.Attendance
            //               join e in Context.Employee on a.EmployeeId equals e.Id
            //               where a.EmployeeId == e.Id
            //               select new EmployeesAttendanceReportViewModel()
            //               {
            //                   EmployeeName = e.FirstName + ' ' + e.LastName,
            //                   EmployeeAttendance = Context.Attendance.Where(a => a.EmployeeId == e.Id).ToList()
            //               }).ToList();

            ViewData["EmployeeList"] = new SelectList(Context.Employee, "Id", "FirstName");

            return View(employeesAttendances);
        }

        [HttpPost]
        public IActionResult Index(Attendance attendance)
        {
            
            this.Context.Attendance.Add(attendance);
            this.Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
