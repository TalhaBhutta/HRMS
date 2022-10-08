using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using HRMS.Data;
using HRMS.Models;
using HRMS.ViewModels;
using System.Security.Claims;

namespace HRMS.Customers
{
    public class CustomerAttendanceReport : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();
        //private EmployeeDBContext empdb = new EmployeeDBContext();
        public List<string> AllDaysOfWeekList = new List<string>();
        public ActionResult xIndex()
        {
        //    return View();
        //}

        //public ActionResult AttendanceTable()
        //{
            var cId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Customer c = db.Customer.FirstOrDefault(u => u.UserId == cId);

            //Get current month number, you can pass this value to controller ActionMethod also
            //int Month = DateTime.Now.Month;
            int Month = 10;

            //int Year = DateTime.Now.Year;
            int Year = 2022;

            int DaysInmonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            //bool[] attendedArray = new bool[DaysInmonth];

            //int[] indexes = new int[DaysInmonth];

            //indexes = db.Attendance.Where(a => a.EmployeeId == e.Id).Select(x => x.CheckInTime.Value.Day).ToArray()


            //attendedArray.SetValue(true, indexes);


            //CustomerAttendanceReportViewModel empWithDate = new CustomerAttendanceReportViewModel
            //{
            //    //Id = a.Id,
            //    //Date = a.CheckInTime,
            //    //Emp_name = empdb.Employee.Single(x => x.EmployeeId == a.EmployeeId).FirstName + " " +
            //    //                   empdb.Employee.Single(x => x.EmployeeId == a.EmployeeId).LastName

            //    //empAtt = db.Attendance.Where(a => a.CheckInTime.Value.Month == Month),
            //    //empList = empdb.Employee.Where(x => x.EmployeeId == empAtt.EmployeeId)
            //    Attendance = db.Attendance.Where(x => x.EmployeeId == e.Id)
            //};
            //List<int> attDays1;

            var attDays1 =  from a in db.Attendance
                            join b in db.Assignment on a.AssignmentId equals b.Id
                            join s in db.Shift on b.ShiftId equals s.Id
                            join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                            join e in db.Employee on a.EmployeeId equals e.Id
                            where cb.CustomerId == c.Id
                                && a.CheckInTime.Value.Month == Month
                                && a.CheckInTime.Value.Year == Year
                            //select new {Employee_name = e.FirstName + ' ' + e.LastName, AttendedDay = a.CheckInTime.Value.Day };
                            select new { EmployeeID = e.Id, AttendedDay = a.CheckInTime.Value.Day };


            //attDays1.Where(x => x. == false);
            //var results1 = (from a in attDays1
            //                select new CustomerAttendanceReportViewModel()
            //               {Emp_name = a.Employee_name

            //                }).ToList();
            var results2 = (from a in db.Attendance
                           join b in db.Assignment on a.AssignmentId equals b.Id
                           join s in db.Shift on b.ShiftId equals s.Id
                           join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                           join e in db.Employee on a.EmployeeId equals e.Id
                           where cb.CustomerId == c.Id
                             && a.CheckInTime.Value.Month == Month
                             && a.CheckInTime.Value.Year == Year
                            select new CustomerAttendanceReportViewModel()
                           {
                               Emp_name = e.FirstName + ' ' + e.LastName,
                               empAttendances = db.Attendance.Where(a => a.EmployeeId == e.Id).ToList()
                           }).ToList();

            var results = (from a in db.Attendance
                           join b in db.Assignment on a.AssignmentId equals b.Id
                           join s in db.Shift on b.ShiftId equals s.Id
                           join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                           join e in db.Employee on a.EmployeeId equals e.Id
                           where cb.CustomerId == c.Id
                                && a.CheckInTime.Value.Month == Month
                                && a.CheckInTime.Value.Year == Year
                           //group p.car by p.PersonId into g
                           //group new { e.FirstName, e.LastName, a.CheckInTime } by e.Id
                           //into g
                           //group e.FirstName + ' ' + e.LastName by e.Id into g

                           //group e by e.FirstName + ' ' + e.LastName into newGroup
                           select new CustomerAttendanceReportViewModel()
                           {
                               //attended = new bool[DaysInmonth],

                               Emp_name = e.FirstName + ' ' + e.LastName,
                               //Date = a.CheckInTime.Value.Date,
                               //InTime = a.CheckInTime.Value.TimeOfDay,
                               //OutTime = a.CheckInTime.Value.TimeOfDay,
                               //empAttendances = db.Attendance.Where(a => a.EmployeeId == e.Id).ToList(),
                               //attDays = db.Attendance.Where(a => a.EmployeeId == e.Id).Select(a => a.CheckInTime.Value.Day).ToList()
                        //attDays = attDays1.Where(a => a.EmployeeID == e.Id).Select(x => x.AttendedDay).ToList()
                        attDays = (from a in db.Attendance
                                  join b in db.Assignment on a.AssignmentId equals b.Id
                                  join s in db.Shift on b.ShiftId equals s.Id
                                  join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                                  join e in db.Employee on a.EmployeeId equals e.Id
                                  where cb.CustomerId == c.Id
                                      && a.CheckInTime.Value.Month == Month
                                      && a.CheckInTime.Value.Year == Year
                                  //select new {Employee_name = e.FirstName + ' ' + e.LastName, AttendedDay = a.CheckInTime.Value.Day };
                                  select new { a.CheckInTime.Value.Day }
                                  ).Select(x => x.Day).ToList()
                               //attDays = (List<int>)(from a in db.Attendance
                               //                       join b in db.Assignment on a.AssignmentId equals b.Id
                               //                       join s in db.Shift on b.ShiftId equals s.Id
                               //                       join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                               //                       join e2 in db.Employee on a.EmployeeId equals e.Id
                               //                       where cb.CustomerId == c.Id
                               //                            && a.CheckInTime.Value.Month == Month
                               //                            && a.CheckInTime.Value.Year == Year
                               //                       select new { attendedDay = a.CheckInTime.Value.Day })
                               //attDays = attDays1.Where(x => x.empId = e.Id)
                               //db.Attendance.Where(z => z.EmployeeId == e.Id ).Where    .Select(x => x.CheckInTime.Value.Day).ToList()

                               //indexes = db.Attendance.Where(a => a.EmployeeId == e.Id).Select(x => x.CheckInTime.Value.Day).ToArray(),
                               //attended = attendedArray
                               //attended = new bool[DaysInmonth],

                           }).ToList();


            //Create List
            //List<Attendance> empWithDate = new List<Attendance>();

            //using (var context = new StudentsEntities())
            //{
            // get emp Name, Id, Date time and order it in ascending order of date
            //empWithDate = db.Attendance.Where(a => a.CheckInTime.Value.Month == Month)
            //        .Select(a =>
            //        new Attendance
            //        {
            //            Id = a.Id,
            //            Date = a.CheckInTime,
            //            Emp_name = empdb.Employee.Single(x => x.EmployeeId == a.EmployeeId).FirstName + " " +
            //                       empdb.Employee.Single(x => x.EmployeeId == a.EmployeeId).LastName
            //        }).OrderBy(a => a.Date).ToList();

            //}

            //return View(empWithDate);
            return View(results);

        }

        public IActionResult Index(string SelectedMonth, string SelectedYear)
        {
            var cId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Customer c = db.Customer.FirstOrDefault(u => u.UserId == cId);

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


            var results = (from a in db.Attendance
                           join b in db.Assignment on a.AssignmentId equals b.Id
                           join s in db.Shift on b.ShiftId equals s.Id
                           join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                           join e in db.Employee on a.EmployeeId equals e.Id
                           where cb.CustomerId == c.Id
                                && a.CheckInTime.Value.Month == mon
                                && a.CheckInTime.Value.Year == year

                           select new CustomerAttendanceReportViewModel()
                           {
                               Emp_name = e.FirstName + ' ' + e.LastName,
                              
                               attDays = (from a in db.Attendance
                                          join b in db.Assignment on a.AssignmentId equals b.Id
                                          join s in db.Shift on b.ShiftId equals s.Id
                                          join cb in db.CustomerBranch on s.BranchId equals cb.BranchId
                                          join e in db.Employee on a.EmployeeId equals e.Id
                                          where cb.CustomerId == c.Id
                                              && a.CheckInTime.Value.Month == mon
                                              && a.CheckInTime.Value.Year == year
                                          //select new {Employee_name = e.FirstName + ' ' + e.LastName, AttendedDay = a.CheckInTime.Value.Day };
                                          select new { a.CheckInTime.Value.Day }
                                  ).Select(x => x.Day).ToList(),

                               empAttendances = db.Attendance.Where(a => a.EmployeeId == e.Id && a.CheckInTime.Value.Month == mon && a.CheckInTime.Value.Year == year).ToList()


                           }).ToList();

            return View(results);
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

                if (year != DateTime.Now.Year && mon != DateTime.Now.Month)
                {
                    noOfDays = TotalNumberOfDaysInMonth(year, mon);
                }
                else
                {
                    noOfDays = DateTime.Now.Day;
                }


                var attendanceList = new List<Attendance>();
                for (int i = 1; i <= noOfDays; i++)
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
        public ActionResult Lucid()
        {
            return View("~/Views/CustomerAttendanceReport/lucidAttendance.cshtml");

        }

        public ActionResult smarthr()
        {
            return View("~/Views/CustomerAttendanceReport/smarthr.cshtml");

        }

        [ActionName("attendance-report")]
        public ActionResult attendancereport()
        {
            return View();
        }
    }
}
