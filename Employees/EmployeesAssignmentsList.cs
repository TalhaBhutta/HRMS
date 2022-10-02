using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Assignments
{
    public class EmployeesAssignmentsList : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("employees-assignments-list")]
        public ActionResult Employeesassignmentslist()
        {
            return View();
        }
    }
}
