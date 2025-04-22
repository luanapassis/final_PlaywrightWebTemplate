using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightWebTemplate.Helpers
{
    public static class PlaywrightSessionHelpers
    {
        public static async Task<IBrowserContext> CreateContextAsync(IBrowser browser)
        {
            return await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
                Locale = JsonHelpers.GetParameterAppSettings("BROWSER_LOCALE"),
                BaseURL = JsonHelpers.GetParameterAppSettings("URL")
            });
        }

        public static async Task<IPage> CreatePageAsync(IBrowserContext context)
        {
            var page = await context.NewPageAsync();

            float timeout = float.TryParse(JsonHelpers.GetParameterAppSettings("DEFAULT_TIMEOUT"), out var result)
                ? result
                : 60000;

            context.SetDefaultTimeout(timeout);
            context.SetDefaultNavigationTimeout(timeout);

            return page;
        }

        public static async Task NavigateToInitialPageAsync(IPage page)
        {
            await page.GotoAsync("");
        }
    }
}
