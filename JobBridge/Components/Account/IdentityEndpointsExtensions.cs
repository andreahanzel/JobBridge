using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using JobBridge.Data.Models; // Make sure this points to your ApplicationUser
using System.Security.Claims; // Needed for ClaimsPrincipal
using Microsoft.AspNetCore.Http; // Needed for HttpContext
using Microsoft.AspNetCore.Mvc; // Needed for [FromForm]

namespace Microsoft.AspNetCore.Builder
{
    // IMPORTANT: This namespace must be Microsoft.AspNetCore.Builder for the extension method to work globally
    public static class IdentityEndpointsExtensions
    {
        // Maps all the default /Account/{action} endpoints to use Identity components.
        public static IEndpointRouteBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapLoginDisplay();
            endpoints.MapLogoutDisplay();
            endpoints.MapRegisterDisplay();

            // Add more if needed for password reset, email confirmation etc.
            return endpoints;
        }

        // Maps the /Account/Login endpoint to the Login component
        private static IEndpointConventionBuilder MapLoginDisplay(this IEndpointRouteBuilder endpoints)
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Login", (HttpContext context, SignInManager<ApplicationUser> signInManager, [FromForm] string email, [FromForm] string password, [FromForm] bool rememberMe) =>
            {
                // This is a minimal example; in a real app, you'd use a more robust form
                // and potentially call your AuthService for better separation of concerns.
                var result = signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    // This is where a successful login would redirect to.
                    // For now, it just redirects to the home page.
                    return Results.LocalRedirect("/");
                }
                // For demonstration, we'll just challenge, in a real app you'd add errors to the form model.
                return Results.Challenge();
            });
            return accountGroup;
        }

        // Maps the /Account/Logout endpoint to the Logout component
        private static IEndpointConventionBuilder MapLogoutDisplay(this IEndpointRouteBuilder endpoints)
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.LocalRedirect("/"); // Redirect to home after logout
            });
            return accountGroup;
        }

        // Maps the /Account/Register endpoint to the Register component
        private static IEndpointConventionBuilder MapRegisterDisplay(this IEndpointRouteBuilder endpoints)
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Register", async (HttpContext context, UserManager<ApplicationUser> userManager, [FromForm] string email, [FromForm] string password) =>
            {
                // Minimal example; integrate with your AuthService and better form handling
                var user = new ApplicationUser { UserName = email, Email = email };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // For now, redirect to login page after successful registration
                    return Results.LocalRedirect("/Account/Login");
                }
                // For demonstration, we'll just challenge. In a real app, you'd show registration errors.
                return Results.Challenge();
            });
            return accountGroup;
        }
    }
}