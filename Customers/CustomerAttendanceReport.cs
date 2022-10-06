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

        public IActionResult Index()
        {
            return View();
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
