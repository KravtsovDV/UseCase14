using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Localization;

namespace UseCase14
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex LocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$",
                                                            RegexOptions.IgnoreCase);

        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var url = httpContext.Request.Path;

            // Right now it's not possible to use httpContext.GetRouteData()
            // since it uses IRoutingFeature placed in httpContext.Features when
            // Routing Middleware registers. It's not set when the Localization Middleware
            // is called, so this example simply assumes the locale will always 
            // be located in the first segment of a path, like in /en-US/Test/Index
            var parts = url.Value?.Split('/') ?? new string[] { "" };
            if (parts.Length >= 2 && LocalePattern.IsMatch(parts[1]))
            {
                var culture = parts[1];

                CultureInfo? locale = Options?.SupportedCultures?.FirstOrDefault(x => x.Name.StartsWith(culture));
                if (locale is not null)
                {
                    culture = locale.Name;
                    return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture));
                }
            }

            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(Options?.DefaultRequestCulture.Culture.Name, Options?.DefaultRequestCulture.UICulture.Name));
        }
    }
}
