using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Calendar
{
    public class EmployeesCalendar : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
