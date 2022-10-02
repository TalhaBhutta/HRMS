using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Attendances
{
    public class AttendanceGrid : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("attendance-grid")]
        public ActionResult attendancegrid()
        {
            return View();
        }
    }
}
