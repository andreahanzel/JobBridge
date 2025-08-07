using JobBridge.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JobBridge.Identity
{
    public class IdentityUserAccessor
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<User> _userManager;

        public IdentityUserAccessor(AuthenticationStateProvider authenticationStateProvider, UserManager<User> userManager)
        {
            _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

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

        public async Task<User> GetRequiredUserAsync()
        {
            var user = await GetUserAsync();
            return user ?? throw new InvalidOperationException("The current user could not be retrieved.");
        }
    }
}