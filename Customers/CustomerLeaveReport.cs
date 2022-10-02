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
    public class CustomerLeaveReport : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();
        //private EmployeeDBContext empdb = new EmployeeDBContext();

        public ActionResult Index()
        {
        //    return View();
        //}

        //public ActionResult AttendanceTable()
        //{
            var cId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Customer c = db.Customer.FirstOrDefault(u => u.UserId == cId);

            //Get current month number, you can pass this value to controller ActionMethod also
            int Month = DateTime.Now.Month;

            int Year = DateTime.Now.Year;

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
            var results = (from a   in db.Attendance
                           join b   in db.Assignment     on a.AssignmentId  equals b.Id
                           join s   in db.Shift          on b.ShiftId       equals s.Id
                           join cb  in db.CustomerBranch on s.BranchId      equals cb.BranchId
                           join e   in db.Employee       on a.EmployeeId    equals e.Id
                           where cb.CustomerId == c.Id   
                                &&  a.CheckInTime.Value.Month == Month
                                &&  a.CheckInTime.Value.Year == Year
                           //group p.car by p.PersonId into g
                           //group new { e.FirstName, e.LastName, a.CheckInTime } by e.Id  into g
                           select new CustomerAttendanceReportViewModel()
                           {
                               Emp_name = e.FirstName + ' ' + e.LastName,
                               //Date = a.CheckInTime.Value.Date,
                               //InTime = a.CheckInTime.Value.TimeOfDay,
                               //OutTime = a.CheckInTime.Value.TimeOfDay,
                               empAttendances = db.Attendance.Where(a => a.EmployeeId == e.Id).ToList()
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

        public ActionResult Lucid()
        {
            return View("~/Views/CustomerLeaveReport/lucid.cshtml");

        }

        public ActionResult smarthr()
        {
            return View("~/Views/CustomerLeaveReport/smarthr.cshtml");

        }

        public ActionResult Fixedcolumns()
        {
            return View("~/Views/DatatablesController/Index.cshtml");

        }

        [ActionName("Leave-report")]
        public ActionResult leavereport()
        {
            return View();
        }
    }
}
