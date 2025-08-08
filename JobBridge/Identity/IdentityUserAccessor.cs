using JobBridge.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JobBridge.Identity
{
    public class IdentityUserAccessor
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;  // Authentication state provider
        private readonly UserManager<User> _userManager; // User manager for managing user accounts

    // Constructor that initializes the IdentityUserAccessor
        public IdentityUserAccessor(AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager)
        {
            _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // Method to get the current user
        public async Task<User?> GetUserAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return await _userManager.FindByIdAsync(userId);
        }

        // Method to get the required user
        public async Task<User> GetRequiredUserAsync()
        {
            var user = await GetUserAsync();
            return user ?? throw new InvalidOperationException("The current user could not be retrieved.");
        }
    }
}