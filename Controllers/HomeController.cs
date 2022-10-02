using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Models;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            //UserManager<ApplicationUser> userManager,
            //SignInManager<ApplicationUser> signInManager,
            ILogger<HomeController> logger)
        {
            //_userManager = userManager;
            //_signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //User.AddIdentity()
                return RedirectToAction("Index", "index", new { area = "" }); //HRMS Index
                //return RedirectToAction("Index4", "EmployeesCreate"); //HRMS Index
            }
            else
            {
                return View(); //Login Index
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
