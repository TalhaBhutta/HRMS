﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Attendances
{
    public class AttendanceReport : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("attendance-report")]
        public ActionResult attendancereport()
        {
            return View();
        }
    }
}
