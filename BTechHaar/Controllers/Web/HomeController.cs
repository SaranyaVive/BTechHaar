using BTechHaar.Models.Models.Web;
using BTechHaar.Main.Services;
using Microsoft.AspNetCore.Mvc;
using BTechHaar.Main.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Options;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace BTechHaar.Main.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IUserLogService _userLogService;
        private readonly User _user;
        public HomeController(IUserLogService userLogService, IOptions<User> user)
        {
            _userLogService = userLogService;
            _user = user.Value;
        }

        public async Task<IActionResult> Index()
        {
            List<UserLogsVM> logs = new List<UserLogsVM>();
            logs = await _userLogService.GetUserLogs();
            return View(logs);
        }
        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(User user)
        {
            
            if (_user.username == user.username && _user.password == user.password)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login.");
                return View(user);
            }
        }

        public async Task<IActionResult> UserList()
        {
            List<UserVM> logs = new List<UserVM>();
            logs = await _userLogService.GetUserList();
            return View(logs);
        }

        public async Task<IActionResult> Dashboard()
        {
            DashboardVM dashboard = new DashboardVM();
            var logs = await _userLogService.GetUserLogs();
            dashboard.logCountVM = logs.GroupBy(n => n.LogDate)
                         .Select(n => new LogCountVM
                         {
                             LogDate = n.Key,
                             LogCount = n.Count()
                         })
                         .OrderBy(n => n.LogDate).ToList();
            dashboard.userCountVM = logs.GroupBy(n => n.LogDate)
                         .Select(n => new UserCountVM
                         {
                             LogDate = n.Key,
                             LogCount = n.Count()
                         })
                         .OrderBy(n => n.LogDate).ToList();
            return View(dashboard);
        }
    }
}
