using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using JobBridge.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Builder
{
    public static class IdentityEndpointsExtensions
    {
        public static IEndpointRouteBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapLoginDisplay();
            endpoints.MapLogoutDisplay();
            endpoints.MapRegisterDisplay();

            return endpoints;
        }

        private static IEndpointConventionBuilder MapLoginDisplay(this IEndpointRouteBuilder endpoints)
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Login", (HttpContext context, SignInManager<User> signInManager, [FromForm] string email, [FromForm] string password, [FromForm] bool rememberMe) =>
            {
                var result = signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false).Result;
                if (result.Succeeded)
                {
                    return Results.LocalRedirect("/");
                }
                return Results.Challenge();
            });
            return accountGroup;
        }

        private static IEndpointConventionBuilder MapLogoutDisplay(this IEndpointRouteBuilder endpoints)
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<User> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.LocalRedirect("/");
            });
            return accountGroup;
        }

        private static IEndpointConventionBuilder MapRegisterDisplay(this IEndpointRouteBuilder endpoints)
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Register", async (HttpContext context, UserManager<User> userManager, [FromForm] string email, [FromForm] string password, [FromForm] string firstName, [FromForm] string lastName, [FromForm] string phone) =>
            {
                var user = new User
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Role = "User", // Default role for new users
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return Results.LocalRedirect("/Account/Login");
                }
                return Results.Challenge();
            });
            return accountGroup;
        }
    }
}