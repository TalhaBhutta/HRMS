﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Index
{
    public class EmployeesIndex : Controller
    {
        public ActionResult Index()
        {
            string strName = Contants.LAYOUT_VERTICAL;
            string strWelcomeText = "Dashboard";

            if (TempData["ModeName"] != null)
                strName = TempData["ModeName"].ToString();

            if (TempData["WelcomeText"] != null)
                strWelcomeText = TempData["WelcomeText"].ToString();

            ViewBag.ModeName = strName;
            ViewBag.WelcomeText = strWelcomeText;

            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "index", new { area = "" }); //HRMS Index
            //}
            //else
            //{
                return View(); //Login Index
            //}
        }

        [HttpPost]
        public ActionResult ValidateLogin(string email, string password)
        {
            //You can fetch email and password from db or API here.
            string dbEmail = "Test";
            string dbPassword = "123";
            bool IsValidUser = false;

            if (email == dbEmail && password == dbPassword)
                IsValidUser = true;

            return Json(new
            {
                IsValidUser = IsValidUser
            });
        }

    }
}
