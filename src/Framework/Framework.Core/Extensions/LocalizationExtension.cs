using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Framework.Core
{
    public static class LocalizationExtension
    {
        public static void UseLocalization(this IApplicationBuilder app)
        {
            var supporttedCultures = new List<CultureInfo>
                {
                    new CultureInfo(FrameworkConstant.LANGUAGE_CHINESE),
                    new CultureInfo( FrameworkConstant.LANGUAGE_ENGLISH),
                };

            app.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(FrameworkConstant.LANGUAGE_CHINESE);
                options.SupportedCultures = supporttedCultures;
                options.SupportedUICultures = supporttedCultures;
                options.AddInitialRequestCultureProvider(new LangCultrueProvider());
            });
        }
    }

    public class LangCultrueProvider : RequestCultureProvider
    {
        /// <inheritdoc />
        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext?.Request.Headers.TryGetValue(FrameworkConstant.LANGUAGE, out var value) == true && !string.IsNullOrEmpty(value))
            {
                var culture = value.ToString().Trim();
                return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture));
            }

            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(FrameworkConstant.LANGUAGE_CHINESE));
        }
    }
}