using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Assignments
{
    public class RecrutersAssignmentsList : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("recrutersassignments-list")]
        public ActionResult recrutersassignmentslist()
        {
            return View();
        }
    }
}
