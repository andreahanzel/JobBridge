using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using JobBridge.Data;
using JobBridge.Identity;

namespace JobBridge.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        // New Login method to be called from the Blazor component
        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await FindUserByEmailAsync(email);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Console.WriteLine($"AuthService: User {email} successfully logged in.");
            }
            else
            {
                Console.WriteLine($"AuthService: Failed login attempt for {email}.");
            }
            return result;
        }

        public async Task<User?> FindUserByEmailAsync(string email)
        {
            Console.WriteLine($"AuthService: Attempting to find user with email: {email}");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(email);
            }
            return user;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            Console.WriteLine($"AuthService: Attempting registration for {user.Email}");
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded && user.Role != null)
            {
                await _userManager.AddToRoleAsync(user, user.Role);
                Console.WriteLine($"AuthService: User {user.Email} registered with role {user.Role}");
            }
            return result;
        }

        public async Task LogoutAsync()
        {
            Console.WriteLine("AuthService: User logged out");
            await _signInManager.SignOutAsync();
        }
    }
}
