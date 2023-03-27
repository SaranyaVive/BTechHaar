using BTechHaar.Models.Models.Web;
using BTechHaar.Main.Services;
using Microsoft.AspNetCore.Mvc;

namespace BTechHaar.Main.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IUserLogService _userLogService;
        public HomeController(IUserLogService userLogService)
        {
            _userLogService = userLogService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserLogsVM> logs = new List<UserLogsVM>();
            logs = await _userLogService.GetUserLogs();
            return View(logs);
        }
    }
}
