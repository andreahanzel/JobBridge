using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JobBridge.Data.Models;

namespace JobBridge.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            Console.WriteLine($"Attempting login for {email}");
            // This should be replaced with the actual Identity calls:
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
        {
            Console.WriteLine($"Attempting registration for {user.Email}");
            // This should be replaced with the actual Identity calls:
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task LogoutAsync()
        {
            Console.WriteLine("User logged out");
            await _signInManager.SignOutAsync();
        }
    }
}