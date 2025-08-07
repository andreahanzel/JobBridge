using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace JobBridge.Identity
{
    public class IdentityRedirectManager
    {
        private readonly NavigationManager _navigationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityRedirectManager(NavigationManager navigationManager, IHttpContextAccessor httpContextAccessor)
        {
            _navigationManager = navigationManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void RedirectTo(string uri)
        {
            // Always use NavigationManager in Blazor Server to avoid headers issues
            _navigationManager.NavigateTo(uri, forceLoad: true);
        }

        public void RedirectToWithStatus(string uri, string message)
        {
            var uriWithQuery = QueryHelpers.AddQueryString(uri, StatusMessageParam, message);
            RedirectTo(uriWithQuery);
        }

        public void RedirectToCurrentPageWithStatus(string message)
        {
            RedirectToWithStatus(_navigationManager.Uri, message);
        }

        public const string StatusMessageParam = "StatusMessage";
    }

    public static class QueryHelpers
    {
        public static string AddQueryString(string uri, string name, string value)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query[name] = value;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}