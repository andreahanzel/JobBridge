using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JobBridge.Data;

namespace JobBridge.Controllers // Handles authentication-related actions
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase // Handles authentication-related actions
    {
        private readonly SignInManager<User> _signInManager; // Manages user sign-in
        private readonly UserManager<User> _userManager; // Manages user accounts

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager) // Constructor
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("signin")] // SignIn endpoint
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request) // SignIn action
        {
            Console.WriteLine($"API SignIn called for email: {request.Email}"); // Log the email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.Email); // Try finding by username
            }
            
            if (user != null)
            {
                Console.WriteLine($"User found: {user.Email}, signing in..."); // Log user sign-in
                await _signInManager.SignInAsync(user, request.Remember);
                Console.WriteLine("SignIn completed successfully");
                return Ok();
            }
            Console.WriteLine("User not found");
            return BadRequest();
        }
    }

// SignInRequest model that represents the user's sign-in credentials
    public class SignInRequest
    {
        public string Email { get; set; } = string.Empty;
        public bool Remember { get; set; }
    }
}