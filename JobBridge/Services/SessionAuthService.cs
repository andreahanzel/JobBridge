using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace JobBridge.Services
{
    public class SessionAuthService : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity()); // Initialize with empty claims

        public SessionAuthService()
        {
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser)); // Return the current authentication state
        }

        // Initialize the authentication state
        public async Task InitializeAsync(IJSRuntime jsRuntime)
        {
            try
            {
                // Add a small delay to ensure JS runtime is ready
                await Task.Delay(100);
                
                // Check sessionStorage first, then localStorage
                var authData = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authData");
                if (string.IsNullOrEmpty(authData))
                {
                    authData = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "authData");
                }
                if (!string.IsNullOrEmpty(authData))
                {
                    var userData = JsonSerializer.Deserialize<UserData>(authData);
                    if (userData != null)
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, userData.UserId),
                            new Claim(ClaimTypes.Name, userData.Name),
                            new Claim(ClaimTypes.Email, userData.Email),
                            new Claim(ClaimTypes.Role, userData.Role),
                            new Claim(ClaimTypes.GivenName, userData.FirstName)
                        };

                        var identity = new ClaimsIdentity(claims, "Custom");
                        _currentUser = new ClaimsPrincipal(identity);
                        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    }
                }
            }
            catch
            {
                // If localStorage fails, continue with empty user
            }
        }

        /// <summary>
        /// Signs in the user and stores their information in localStorage.
        /// </summary>
        public async Task SignInAsync(IJSRuntime jsRuntime, string userId, string email, string name, string role, string firstName, bool remember = false)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.GivenName, firstName)
            };

            var identity = new ClaimsIdentity(claims, "Custom");
            _currentUser = new ClaimsPrincipal(identity);

            // Always store in sessionStorage (for current session)
            // Use localStorage only if remember is true
            var userData = new UserData
            {
                UserId = userId,
                Email = email,
                Name = name,
                Role = role,
                FirstName = firstName
            };

            var authData = JsonSerializer.Serialize(userData);
            
            if (remember)
            {
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", "authData", authData);
            }
            else
            {
                await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authData", authData);
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// Signs out the user and removes their information from localStorage.
        /// </summary>
        public async Task SignOutAsync(IJSRuntime jsRuntime)
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authData");
            await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authData");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// User data stored in localStorage.
        /// </summary>
        private class UserData
        {
            public string UserId { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
        }
    }
}