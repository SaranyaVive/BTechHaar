using BTechHaar.Models.API.Request;
using BTechHaar.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BTechHaar.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("userauthenticate")]
        public async Task<IActionResult> UserLogin(LoginRequest request)
        {
            try
            {
                var userchek = await _accountService.CheckValidLogin(request);
                return Ok(userchek);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("registeruser")]
        public async Task<IActionResult> RegisterUser(SignupRequest request)
        {
            try
            {
                var signUp = await _accountService.RegisterUser(request);
                return Ok(signUp);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("verifyemail")]
        public async Task<IActionResult> VerifyEmail(int UserId)
        {
            try
            {
                await _accountService.VerifyEmail(UserId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
