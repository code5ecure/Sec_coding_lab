using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Models;

namespace UserProfileApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            return result.Succeeded ? Ok() : Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestReset([FromBody] PasswordResetRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Ok(); // Don't expose
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(new { token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> Reset([FromBody] PasswordResetDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        public class RegisterDto { public string Email { get; set; } public string Password { get; set; } }
        public class LoginDto { public string Email { get; set; } public string Password { get; set; } }
        public class PasswordResetRequestDto { public string Email { get; set; } }
        public class PasswordResetDto { public string Email { get; set; } public string Token { get; set; } public string NewPassword { get; set; } }
    }
}