using Microsoft.AspNetCore.Identity;
using JobBridge.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace JobBridge.Identity
{
    internal sealed class IdentityUserAccessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityRedirectManager _redirectManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider; // Added this dependency

        public IdentityUserAccessor(
            UserManager<ApplicationUser> userManager,
            IdentityRedirectManager redirectManager,
            AuthenticationStateProvider authenticationStateProvider) // Added this parameter
        {
            _userManager = userManager;
            _redirectManager = redirectManager;
            _authenticationStateProvider = authenticationStateProvider; // Assigned the dependency
        }

        public async Task<ApplicationUser> GetRequiredUserAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);

            if (user is null)
            {
                // Get the current AuthenticationState to pass to the redirect manager
                var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync(); // Get AuthenticationState
                _redirectManager.RedirectToWithStatus(
                    "Account/InvalidUser",
                    $"Error: Unable to load user with ID '{_userManager.GetUserId(principal)}'.",
                    authenticationState); // Pass authenticationState
            }

            return user;
        }
    }
}