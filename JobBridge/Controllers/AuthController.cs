using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JobBridge.Data;

namespace JobBridge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            Console.WriteLine($"API SignIn called for email: {request.Email}");
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.Email);
            }
            
            if (user != null)
            {
                Console.WriteLine($"User found: {user.Email}, signing in...");
                await _signInManager.SignInAsync(user, request.Remember);
                Console.WriteLine("SignIn completed successfully");
                return Ok();
            }
            Console.WriteLine("User not found");
            return BadRequest();
        }
    }

    public class SignInRequest
    {
        public string Email { get; set; } = string.Empty;
        public bool Remember { get; set; }
    }
}