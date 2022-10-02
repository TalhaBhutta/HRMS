using Microsoft.AspNetCore.Mvc;

namespace HRMS.Recruters
{
    public class RecruterAttendanceReport : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
