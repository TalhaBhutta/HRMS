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
        private ApplicationDBContext db = new ApplicationDBContext();
        //private EmployeeDBContext empdb = new EmployeeDBContext();
        public List<string> AllDaysOfWeekList = new List<string>();




        public EmployeeAttendanceController(ApplicationDBContext _context, UserManager<ApplicationUser> userManager)
        {
            this.db = _context;
            _userManager = userManager;

        }

        public IActionResult Index(string SelectedMonth, string SelectedYear, string SelectedEmployee)
        {
            var eId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Employee employee1 = db.Employee.FirstOrDefault(u => u.UserId == eId);

            int mon, year, noOfDays, empID = 0;

            if (SelectedMonth == null || SelectedMonth == "undefined")
            {
                SelectedMonth = DateTime.Now.ToString("MM");
                
            }
            if(SelectedYear == null || SelectedYear == "undefined")
            {
                SelectedYear = DateTime.Now.ToString("yyyy");
            }

            if (SelectedEmployee != null && SelectedEmployee != "all")
            {
                empID = int.Parse(SelectedEmployee);
            }

            mon = int.Parse(SelectedMonth);
            DateTimeOffset dateTime = DateTimeOffset.Now;
            dateTime = new DateTimeOffset(2008, mon, 1, 8, 6, 32,
                                 new TimeSpan(1, 0, 0));

            year = int.Parse(SelectedYear);
            //mon = 9;

            noOfDays = TotalNumberOfDaysInMonth(year, mon);
            AllDaysOfWeekList = GetDaysOfWeek(year, mon, noOfDays);
            ViewBag.data = AllDaysOfWeekList;


            //var attDays1 = from a in db.Attendance
            //               join b in db.Assignment on a.AssignmentId equals b.Id
            //               join s in db.Shift on b.ShiftId equals s.Id
            //               join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
            //               join e in db.Employee on a.EmployeeId equals e.Id
            //               where cb.CustomerId == c.Id
            //                   && a.CheckInTime.Value.Month == mon
            //                   && a.CheckInTime.Value.Year == year
            //               //select new {Employee_name = e.FirstName + ' ' + e.LastName, AttendedDay = a.CheckInTime.Value.Day };
            //               select new { EmployeeID = e.Id, AttendedDay = a.CheckInTime.Value.Day };


            //var results2 = (from a in db.Attendance
            //                join b in db.Assignment on a.AssignmentId equals b.Id
            //                join s in db.Shift on b.ShiftId equals s.Id
            //                join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
            //                join e in db.Employee on a.EmployeeId equals e.Id
            //                where cb.CustomerId == c.Id
            //                  && a.CheckInTime.Value.Month == mon
            //                  && a.CheckInTime.Value.Year == year
            //                select new CustomerAttendanceReportViewModel()
            //                {
            //                    Emp_name = e.FirstName + ' ' + e.LastName,
            //                    empAttendances = db.Attendance.Where(a => a.EmployeeId == e.Id).ToList()
            //                }).ToList();

            //var employeesAttendances = (from a in db.Attendance
            //                            join b in db.Assignment on a.AssignmentId equals b.Id
            //                            join s in db.Shift on b.ShiftId equals s.Id
            //                            join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
            //                            join e in db.Employee on a.EmployeeId equals e.Id
            //                            where cb.CustomerId == c.Id
            //                              && a.CheckInTime.Value.Month == mon
            //                              && a.CheckInTime.Value.Year == year
            //                            select new CustomerAttendanceReportViewModel()
            //                            {
            //                                Emp_name = e.FirstName + ' ' + e.LastName,
            //                                empAttendances = db.Attendance.Where(a => a.EmployeeId == e.Id && a.CheckInTime.Value.Month == mon && a.CheckInTime.Value.Year == year).ToList()
            //                            }).ToList();
            //int ds = 5;
            //.Where(a => a.EmployeeId == ds)

            ViewData["EmployeeList"] = new SelectList(db.Employee.Where(x => x.Id == employee1.Id), "Id", "FirstName", SelectedEmployee);
            ViewData["LocationList"] = new SelectList(db.Locations, "Id", "Address");

            //var employeesAttendances = (from a in Context.Attendance
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


            //var employeesAttendances = (from a in Context.Attendance
            //               join b in Context.Assignment on a.AssignmentId equals b.Id
            //               join s in Context.Shift on b.ShiftId equals s.Id
            //               join cb in Context.Recruter on s.BranchId equals cb.
            //               join e in Context.Employee on a.EmployeeId equals e.Id
            //               where cb.CustomerId == c.Id
            //                 && a.CheckInTime.Value.Month == Month
            //                 && a.CheckInTime.Value.Year == Year
            //               select new EmployeesAttendanceReportViewModel()
            //               {
            //                   Emp_name = e.FirstName + ' ' + e.LastName,
            //                   empAttendances = Context.Attendance.Where(a => a.EmployeeId == e.Id).ToList()
            //               }).ToList();

            var employeesAttendances = (from a in db.Attendance
                                        join e in db.Employee on a.EmployeeId equals e.Id
                                        where a.EmployeeId == employee1.Id
                                   && a.CheckInTime.Value.Month == mon
                                   && a.CheckInTime.Value.Year == year
                                   && a.EmployeeId == employee1.Id
                                        select new EmployeesAttendanceReportViewModel
                                        {
                                            Emp_name = e.FirstName + ' ' + e.LastName,
                                            Selectedmonth = SelectedMonth,
                                            Selectedyear = year,
                                            Selectedemp = employee1.Id,
                                            //attDays = (from a in db.Attendance
                                            //           join b in db.Assignment on a.AssignmentId equals b.Id
                                            //           join s in db.Shift on b.ShiftId equals s.Id
                                            //           join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                                            //           join e in db.Employee on a.EmployeeId equals e.Id
                                            //           where cb.CustomerId == c.Id
                                            //               && a.CheckInTime.Value.Month == mon
                                            //               && a.CheckInTime.Value.Year == year
                                            //           //select new {Employee_name = e.FirstName + ' ' + e.LastName, AttendedDay = a.CheckInTime.Value.Day };
                                            //           select new { a.CheckInTime.Value.Day }
                                            //   ).Select(x => x.Day).ToList(),
                                            empAttendances = db.Attendance.Where(a => a.EmployeeId == employee1.Id && a.CheckInTime.Value.Month == mon && a.CheckInTime.Value.Year == year).OrderBy(e => e.CheckInTime).ToList(),
                                        }).ToList();


            

            return View(employeesAttendances);
        }

        [HttpPost]
        public IActionResult MarkAttendance(Attendance attendance)
        {

            if (attendance.MarkAttendanceBY == "date")
            {
                string[] values = attendance.multi_date.Split(',');
                var attendanceList = new List<Attendance>();
                for (int i = 0; i < values.Length; i++)
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

                    if (year != DateTime.Now.Year && mon != DateTime.Now.Month)
                    {
                        noOfDays = TotalNumberOfDaysInMonth(year, mon);
                    }
                    else
                    {
                        noOfDays = DateTime.Now.Day;
                    }
                    var atDate = Convert.ToDateTime(values[i]);

                    DateTimeOffset CheckIndateTime = DateTimeOffset.Now;
                    DateTimeOffset CheckOutdateTime = DateTimeOffset.Now;
                    CheckIndateTime = new DateTimeOffset(atDate.Year, atDate.Month, atDate.Day, attendance.CheckInTime.Value.Hour, attendance.CheckInTime.Value.Minute, attendance.CheckInTime.Value.Second,
                                         new TimeSpan(1, 0, 0));
                    CheckOutdateTime = new DateTimeOffset(atDate.Year, atDate.Month, atDate.Day, attendance.CheckOutTime.Value.Hour, attendance.CheckOutTime.Value.Minute, attendance.CheckOutTime.Value.Second,
                                        new TimeSpan(1, 0, 0));

                    attendanceList.Add(new Attendance
                    {
                        Id = attendance.Id,
                        EmployeeId = attendance.EmployeeId,
                        DepartmentID = attendance.DepartmentID,
                        MarkAttendanceBY = attendance.MarkAttendanceBY,
                        Year = attendance.Year,
                        LocationID = attendance.LocationID,
                        Month = attendance.Month,
                        IsLate = attendance.IsLate,
                        IsHalfDay = attendance.IsHalfDay,
                        CheckInTime = CheckIndateTime,
                        CheckOutTime = CheckOutdateTime,
                        AssignmentId = 2,
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
                this.db.Attendance.AddRange(attendanceList); //here you add all new entities to context
                this.db.SaveChanges();
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

                if (year == DateTime.Now.Year && mon == DateTime.Now.Month)
                {
                    noOfDays = DateTime.Now.Day;

                }
                else
                {
                    noOfDays = TotalNumberOfDaysInMonth(year, mon);
                }


                var EmpAttendanceList = db.Attendance.Where(x => x.EmployeeId == attendance.EmployeeId && x.CheckInTime.Value.Month == mon && x.CheckOutTime.Value.Year == year).ToList();
                db.Attendance.RemoveRange(EmpAttendanceList);
                db.SaveChanges();

                //var Attendances = db.Attendance.Where(a => a. == "david");
                //context.Users.Delete(u => u.FirstName == "firstname");
                //db.Attendance.Remove(Attendances);
                //db.Attendance.SubmitChanges();

                var attendanceList = new List<Attendance>();
                for (int i = 1; i <= noOfDays; i++)
                {
                    DateTimeOffset dateTime = DateTimeOffset.Now;
                    dateTime = new DateTimeOffset(int.Parse(attendance.Year), mon, i, attendance.CheckInTime.Value.Hour, attendance.CheckInTime.Value.Minute, attendance.CheckInTime.Value.Second,
                                         new TimeSpan(1, 0, 0));
                    DateTimeOffset CheckIndateTime = DateTimeOffset.Now;
                    DateTimeOffset CheckOutdateTime = DateTimeOffset.Now;
                    CheckIndateTime = new DateTimeOffset(int.Parse(attendance.Year), mon, i, attendance.CheckInTime.Value.Hour, attendance.CheckInTime.Value.Minute, attendance.CheckInTime.Value.Second,
                                         new TimeSpan(1, 0, 0));
                    CheckOutdateTime = new DateTimeOffset(int.Parse(attendance.Year), mon, i, attendance.CheckOutTime.Value.Hour, attendance.CheckOutTime.Value.Minute, attendance.CheckOutTime.Value.Second,
                                        new TimeSpan(1, 0, 0));


                    attendanceList.Add(new Attendance
                    {
                        Id = attendance.Id,
                        EmployeeId = attendance.EmployeeId,
                        DepartmentID = attendance.DepartmentID,
                        MarkAttendanceBY = attendance.MarkAttendanceBY,
                        Year = attendance.Year,
                        LocationID = attendance.LocationID,
                        Month = attendance.Month,
                        IsLate = attendance.IsLate,
                        IsHalfDay = attendance.IsHalfDay,
                        CheckInTime = CheckIndateTime,
                        CheckOutTime = CheckOutdateTime,
                        AssignmentId = 2,
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
                        multi_date = attendance.multi_date,
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
                this.db.Attendance.AddRange(attendanceList); //here you add all new entities to context
                this.db.SaveChanges();

            }
            //this.Context.Attendance.AddRange(attendanceList);
            //this.Context.SaveChanges();

            //this.Context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditAttendance(Attendance attendance)
        {
            this.db.Update(attendance);
            this.db.SaveChanges();
            return RedirectToAction("Index");
        }
        public int TotalNumberOfDaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public List<string> GetDaysOfWeek(int year, int month, int noOfDays)
        {
            List<string> DaysOfWeekList = new List<string>();

            for (int i = 1; i <= noOfDays; i++)
            {
                DateTime dateValue = new DateTime(year, month, i);
                DaysOfWeekList.Add(dateValue.ToString("ddd"));
            }


            return DaysOfWeekList;
        }



    }
}
