using Microsoft.AspNetCore.Mvc;

namespace BTechHaar.Main.Controllers.Web
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TermAndConditions()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
