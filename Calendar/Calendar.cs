﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Calendar
{
    public class Calendar : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
