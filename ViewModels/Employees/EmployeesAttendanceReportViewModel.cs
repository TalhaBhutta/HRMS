

using HRMS.Models;
using System.Collections.Generic;

namespace HRMS.ViewModels
{
    public class EmployeesAttendanceReportViewModel
    {
        //public int Id { get; set; }
        public string Emp_name { get; set; }
        //public DateTimeOffset? Date { get; set; }
        //public TimeSpan InTime { get; set; }
        //public TimeSpan OutTime { get; set; }
        public List<Attendance> empAttendances { get; set; }
        public List<int> attDays { get; set; }


        public bool[] attended { get; set; }
        public int[] indexes { get; set; }
        public string Selectedmonth { get; set; }
        public int Selectedyear { get; set; }
        public int Selectedemp { get; set; }

    }

    //public class EmployeesAttendanceReportListViewModel
    //{
    //    public List<Attendance> AttendanceList { get; set; }
    //    public List<Employee> EmployeeList { get; set; }

    //}
}
