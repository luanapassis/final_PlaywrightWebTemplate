using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightWebTemplate.Helpers;
using System.Threading.Tasks;

namespace PlaywrightWebTemplate.Helpers
{


    public static class BrowserHelpers
    {
        public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
        {
            string browserName = JsonHelpers.GetParameterAppSettings("BROWSER");

            return browserName.ToLower() switch
            {
                "chromium" => await playwright.Chromium.LaunchAsync(GetDefaultLaunchOptions()),
                "firefox" => await playwright.Firefox.LaunchAsync(GetDefaultLaunchOptions()),
                "webkit" => await playwright.Webkit.LaunchAsync(GetDefaultLaunchOptions()),
                _ => throw new Exception($"❌ Browser '{browserName}' is not supported.")
            };
        }

        private static BrowserTypeLaunchOptions GetDefaultLaunchOptions()
        {
            return new BrowserTypeLaunchOptions
            {
                Headless = bool.TryParse(JsonHelpers.GetParameterAppSettings("HEADLESS"), out var headless) ? headless : (bool?)null, // Convert string to nullable bool
                SlowMo = 0, // Useful for visual debugging
                
            };
        }
    }
}
