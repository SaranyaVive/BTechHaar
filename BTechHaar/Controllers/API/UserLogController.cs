using BTechHaar.Models.Models.API.Request;
using BTechHaar.Main.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTechHaar.Main.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogController : ControllerBase
    {
        private readonly IUserLogService _userLogService;
        public UserLogController(IUserLogService userLogService)
        {
            _userLogService = userLogService;
        }

        [HttpPost]
        [Route("adduserlog")]
        public async Task<IActionResult> AddUserLog(UserLogRequest request)
        {
            try
            {
                await _userLogService.AddUserLog(request);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("adduserbulklogs")]
        public async Task<IActionResult> AddUserBulkLogs(List<UserLogRequest> request)
        {
            try
            {
                await _userLogService.AddUserLogs(request);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
