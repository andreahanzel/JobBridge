using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace JobBridge.Identity
{
    internal sealed class IdentityRedirectManager
    {
        public const string StatusMessageParam = "StatusMessage";
        public const string RedirectUrlParam = "RedirectUrl";

        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public IdentityRedirectManager(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _navigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        [DoesNotReturn]
        public void RedirectTo(string uri)
        {
            _navigationManager.NavigateTo(uri, new NavigationOptions { ForceLoad = true });
            throw new InvalidOperationException("RedirectTo called."); // Added to satisfy [DoesNotReturn]
        }

        [DoesNotReturn]
        public void RedirectToCurrentPage() => RedirectTo(_navigationManager.Uri);

        [DoesNotReturn]
        public void RedirectToCurrentPageWithStatus(string message, AuthenticationState? authenticationState = null)
        {
            var uri = _navigationManager.Uri;
            if (authenticationState is { User.Identity.IsAuthenticated: true })
            {
                uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri).ToString();
            }

            RedirectToWithStatus(uri, message, authenticationState);
            throw new InvalidOperationException("RedirectToCurrentPageWithStatus called."); // Added to satisfy [DoesNotReturn]
        }

        [DoesNotReturn]
        public void RedirectToWithStatus(string uri, string message, AuthenticationState? authenticationState = null)
        {
            uri = QueryHelpers.AddQueryString(uri, StatusMessageParam, message);

            if (authenticationState is { User.Identity.IsAuthenticated: true })
            {
                // No further redirect logic here for simplicity, as discussed
            }

            RedirectTo(uri);
            throw new InvalidOperationException("RedirectToWithStatus called."); // Added to satisfy [DoesNotReturn]
        }
    }

    // Helper class for URL query string manipulation (often provided by Microsoft.AspNetCore.WebUtilities,
    // but included here to avoid an extra dependency if only this method is needed)
    internal static class QueryHelpers
    {
        public static string AddQueryString(string uri, string name, string value)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[name] = value;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}