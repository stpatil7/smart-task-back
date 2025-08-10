using Domain.Dto.LoginDtos;
using Domain.Interfaces.Login;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("/Register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationsDto model)
        {
            try
            {
                var result = await _loginService.RegisterUserAsync(model);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPost("/Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto model)
        {
            var result = await _loginService.Login(model);
            if (result == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(result);
        }
        [HttpPost("/forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var response = await _loginService.ForgotPassword(model);

            return response switch
            {
                "Invalid User" => NotFound("User Not Found"),
                "Otp Sent" => Ok("Reset OTP sent to your email"),
                _ => StatusCode(500, "Someting went wrong")
            };
        }
        [HttpPost("/reset-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var response = await _loginService.ResetPassword(model);

            return response switch
            {
                "OTP expired or not found" => BadRequest("OTP expired or not found"),
                "Invalid OTP" => BadRequest("Invalid OTP"),
                "Invalid User" => NotFound("User not found"),
                "Password reset complete" => Ok("Password has been reset successfully"),
                _ => StatusCode(500, "Something went wrong")
            };
        }
    }
}
