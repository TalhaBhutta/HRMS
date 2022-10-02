using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Attendances
{
    public class AttendanceList : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("attendance-list")]
        public ActionResult attendancelist()
        {
            return View();
        }
    }
}
