using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace JobBridge.Services
{
    public class SessionAuthService : AuthenticationStateProvider
    {
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public SessionAuthService()
        {
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public async Task InitializeAsync(IJSRuntime jsRuntime)
        {
            try
            {
                var authData = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "authData");
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

            if (remember)
            {
                var userData = new UserData
                {
                    UserId = userId,
                    Email = email,
                    Name = name,
                    Role = role,
                    FirstName = firstName
                };

                var authData = JsonSerializer.Serialize(userData);
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", "authData", authData);
            }
            
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignOutAsync(IJSRuntime jsRuntime)
        {
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authData");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

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