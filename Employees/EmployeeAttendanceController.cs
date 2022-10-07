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
            //var cId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Customer c = Context.Customer.FirstOrDefault(u => u.UserId == cId);

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
           
            if(attendance.MarkAttendanceBY == "date")
            {
                string[] values = attendance.multi_date.Split(',');
                var attendanceList = new List<Attendance>();
                for (int i = 0; i < values.Length; i++)
                {
                    attendanceList.Add(new Attendance
                    {
                        Id = attendance.Id,
                        EmployeeId = attendance.EmployeeId,
                        DepartmentID = attendance.DepartmentID,
                        MarkAttendanceBY = attendance.MarkAttendanceBY,
                        Year = attendance.Year,
                        Month = attendance.Month,
                        IsLate = attendance.IsLate,
                        IsHalfDay = attendance.IsHalfDay,
                        CheckInTime = attendance.CheckInTime,
                        CheckOutTime = attendance.CheckOutTime,
                        AssignmentId = attendance.AssignmentId,
                        TimesheetId = attendance.TimesheetId,
                        ManualCheckInLat = attendance.ManualCheckInLat,
                        ManualCheckInLong = attendance.ManualCheckInLong,
                        ManualCheckOutLat = attendance.ManualCheckOutLat,
                        ManualCheckOutLong = attendance.ManualCheckOutLong,
                        ManualOutOfRangeCheckIn = attendance.ManualOutOfRangeCheckIn,
                        ManualOutOfRangeCheckOut = attendance.ManualOutOfRangeCheckOut,
                        CheckInLat = attendance.CheckInLat,
                        CheckInLong = attendance.CheckInLong,
                        CheckOutLat = attendance.CheckOutLat,
                        CheckOutLong = attendance.CheckOutLong,
                        OutOfRangeCheckIn = attendance.OutOfRangeCheckIn,
                        OutOfRangeCheckOut = attendance.OutOfRangeCheckOut,
                        NoShow = attendance.NoShow,
                        InvoiceId = attendance.InvoiceId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = attendance.CreatedBy,
                        ModifiedOn = attendance.ModifiedOn,
                        ModifiedBy = attendance.ModifiedBy,
                        WorkingFrom = attendance.WorkingFrom,
                        multi_date = values[i].Trim()
                        
                    }); //here you create your entity and add it to List


                    //values[i] = values[i].Trim();
                    //attendance.CreatedOn = DateTime.Now;
                    //attendance.multi_date = values[i].Trim();
                    //attendanceList.Add(attendance);
                    //Context.Attendance.Add(attendance);

                    ////Context.SaveChanges();
                    //Context.SaveChanges();

                }
                this.Context.Attendance.AddRange(attendanceList); //here you add all new entities to context
                this.Context.SaveChanges();
            }
            else if (attendance.MarkAttendanceBY == "month")
            {
                int mon, year, noOfDays = 0;

                string SelectedMonth = attendance.Month;
                string SelectedYear = attendance.Year;

                if (SelectedMonth == null || SelectedYear == null)
                {
                    SelectedMonth = DateTime.Now.ToString("MM");
                    SelectedYear = DateTime.Now.ToString("yyyy");
                }



                mon = int.Parse(SelectedMonth);
               

                year = int.Parse(SelectedYear);

                noOfDays = TotalNumberOfDaysInMonth(year, mon);

                var attendanceList = new List<Attendance>();
                for (int i = 0; i < noOfDays; i++)
                {
                    DateTimeOffset dateTime = DateTimeOffset.Now;
                    dateTime = new DateTimeOffset(int.Parse(attendance.Year), mon, i, attendance.CheckInTime.Value.Hour, attendance.CheckInTime.Value.Minute, attendance.CheckInTime.Value.Second,
                                         new TimeSpan(1, 0, 0));
                    
                    attendanceList.Add(new Attendance
                    {
                        Id = attendance.Id,
                        EmployeeId = attendance.EmployeeId,
                        DepartmentID = attendance.DepartmentID,
                        MarkAttendanceBY = attendance.MarkAttendanceBY,
                        Year = attendance.Year,
                        Month = attendance.Month,
                        IsLate = attendance.IsLate,
                        IsHalfDay = attendance.IsHalfDay,
                        CheckInTime = attendance.CheckInTime,
                        CheckOutTime = attendance.CheckOutTime,
                        AssignmentId = attendance.AssignmentId,
                        TimesheetId = attendance.TimesheetId,
                        ManualCheckInLat = attendance.ManualCheckInLat,
                        ManualCheckInLong = attendance.ManualCheckInLong,
                        ManualCheckOutLat = attendance.ManualCheckOutLat,
                        ManualCheckOutLong = attendance.ManualCheckOutLong,
                        ManualOutOfRangeCheckIn = attendance.ManualOutOfRangeCheckIn,
                        ManualOutOfRangeCheckOut = attendance.ManualOutOfRangeCheckOut,
                        CheckInLat = attendance.CheckInLat,
                        CheckInLong = attendance.CheckInLong,
                        CheckOutLat = attendance.CheckOutLat,
                        CheckOutLong = attendance.CheckOutLong,
                        OutOfRangeCheckIn = attendance.OutOfRangeCheckIn,
                        OutOfRangeCheckOut = attendance.OutOfRangeCheckOut,
                        NoShow = attendance.NoShow,
                        InvoiceId = attendance.InvoiceId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = attendance.CreatedBy,
                        ModifiedOn = attendance.ModifiedOn,
                        ModifiedBy = attendance.ModifiedBy,
                        WorkingFrom = attendance.WorkingFrom,
                        multi_date =attendance.multi_date,
                        Day = dateTime
                    }); //here you create your entity and add it to List


                    //values[i] = values[i].Trim();
                    //attendance.CreatedOn = DateTime.Now;
                    //attendance.multi_date = values[i].Trim();
                    //attendanceList.Add(attendance);
                    //Context.Attendance.Add(attendance);

                    ////Context.SaveChanges();
                    //Context.SaveChanges();

                }
                this.Context.Attendance.AddRange(attendanceList); //here you add all new entities to context
                this.Context.SaveChanges();

            }
                //this.Context.Attendance.AddRange(attendanceList);
                //this.Context.SaveChanges();

                //this.Context.SaveChanges();


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
