using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models;
//using HRMS.Temp2;

namespace HRMS.ViewModels
{
    public class CustomerAttendanceReportViewModel
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
        //public IEnumerable<Employee> empList { get; set; }
        //public IEnumerable<Attendance> empAtt { get; set; }

        // //public Employee e { get; set; }
        //// public Location l { get; set; }
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
        // public string Introduction { get; set; }
        // public string PrimaryPhoneNumber { get; set; }
        // public string Email { get; set; }
        // public string LocationName { get; set; }
        // public int AssigmentsCount { get; set; }
        // public int CompletedProjets { get; set; }
        // public string Occupation { get; set; }
        // public decimal TotalRevenue { get; set; }
        // public IEnumerable<Experience> Experiences { get; set; }
        // public IEnumerable<Assignment> Assignments { get; set; }
        // //public IEnumerable<JobCategory> JobCategories { get; set; }

        // //public IEnumerable<Shift> Shifts { get; set; }
    }
}
