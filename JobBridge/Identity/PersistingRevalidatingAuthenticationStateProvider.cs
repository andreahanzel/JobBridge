using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using JobBridge.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobBridge.Identity
{
    internal sealed class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory; // Service scope factory for creating scopes
        private readonly ILogger<PersistingRevalidatingAuthenticationStateProvider> _logger; // Logger for this provider
        private readonly IdentityOptions _options; // Identity options

    // This constructor initializes the PersistingRevalidatingAuthenticationStateProvider
        public PersistingRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            IOptions<IdentityOptions> optionsAccessor)
            : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = loggerFactory.CreateLogger<PersistingRevalidatingAuthenticationStateProvider>();
            _options = optionsAccessor.Value;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30); // Revalidation interval

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validating authentication state for user: {User}", authenticationState.User.Identity?.Name ?? "Anonymous"); // Log the user being validated
            await using var scope = _scopeFactory.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var result = await ValidateSecurityStampAsync(userManager, authenticationState.User);
            _logger.LogInformation("Authentication state validation result: {Result}", result); // Log the result
            return result;
        }

//
        private async Task<bool> ValidateSecurityStampAsync(UserManager<User> userManager, ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal); // Get the user associated with the principal
            if (user == null)
            {
                _logger.LogWarning("User not found for principal: {Principal}", principal.Identity?.Name ?? "Anonymous"); // Log warning if user not found
                return false;
            }
            else if (!userManager.SupportsUserSecurityStamp)
            {
                _logger.LogInformation("Security stamp not supported for user: {User}", user.UserName); // Log information if security stamp is not supported
                return true;
            }
            else
            {
                var principalStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType); // Get the security stamp from the principal
                var userStamp = await userManager.GetSecurityStampAsync(user);
                var isValid = principalStamp == userStamp;
                _logger.LogInformation("Security stamp validation for {User}: PrincipalStamp={PrincipalStamp}, UserStamp={UserStamp}, IsValid={IsValid}",
                    user.UserName, principalStamp, userStamp, isValid);
                return isValid;
            }
        }
    }

    internal sealed class IdentityNoOpEmailSender : IEmailSender<User>
    {
        private readonly ILogger<IdentityNoOpEmailSender> _logger; // Logger for email sending operations

        public IdentityNoOpEmailSender(ILogger<IdentityNoOpEmailSender> logger) => _logger = logger; // Constructor that initializes the logger

        public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            _logger.LogWarning("No-op: Not sending email to {Email} with confirmation link {ConfirmationLink}", email, confirmationLink); // Log warning for no-op email sending
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            _logger.LogWarning("No-op: Not sending email to {Email} with reset link {ResetLink}", email, resetLink); // Log warning for no-op email sending
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            _logger.LogWarning("No-op: Not sending email to {Email} with reset code {ResetCode}", email, resetCode); // Log warning for no-op email sending
            return Task.CompletedTask;
        }
    }
}