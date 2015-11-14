using Microsoft.AspNet.Mvc;

namespace vNext_Durandal.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shell()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
