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
            var user = await FindUserByEmailAsync(email); // Find user by email
            if (user == null)
            {
                return SignInResult.Failed; // User not found
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Console.WriteLine($"AuthService: User {email} successfully logged in."); // Log successful login
            }
            else
            {
                Console.WriteLine($"AuthService: Failed login attempt for {email}."); // Log failed login attempt
            }
            return result;
        }
        // Find user by email
        public async Task<User?> FindUserByEmailAsync(string email)
        {
            Console.WriteLine($"AuthService: Attempting to find user with email: {email}"); // Log the attempt to find user by email

            var user = await _userManager.FindByEmailAsync(email); // Find user by email
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(email); // Find user by username
            }
            return user;
        }

        // Check password sign-in
        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure); // Check password sign-in
        }

        // Register a new user
        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            Console.WriteLine($"AuthService: Attempting registration for {user.Email}"); // Log registration attempt
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded && user.Role != null)
            {
                await _userManager.AddToRoleAsync(user, user.Role);
                Console.WriteLine($"AuthService: User {user.Email} registered with role {user.Role}"); // Log role assignment
            }
            return result;
        }

        // Logout the user
        public async Task LogoutAsync()
        {
            Console.WriteLine("AuthService: User logged out"); // Log user logout
            await _signInManager.SignOutAsync();
        }
    }
}
