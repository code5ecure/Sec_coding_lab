using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Models;


namespace UserProfileApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(new ProfileDto { FullName = user.FullName, Bio = user.Bio });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProfileDto model)
        {
            var user = await _userManager.GetUserAsync(User);
            user.FullName = model.FullName;
            user.Bio = model.Bio;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}