
using Microsoft.Playwright;

namespace PlaywrightWebTemplate.Helpers
{
    public static class PlaywrightSessionHelpers
    {
        public static async Task<IBrowserContext> CreateContextAsync(IBrowser browser)
        {
            bool.TryParse(JsonHelpers.GetParameterAppSettings("RECORD_VIDEO"), out var shouldRecordVideo);

            var contextOptions = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
                Locale = JsonHelpers.GetParameterAppSettings("BROWSER_LOCALE"),
                BaseURL = JsonHelpers.GetParameterAppSettings("URL"),
            };

            if (shouldRecordVideo)
            {
                contextOptions.RecordVideoDir = Path.Combine(ExtentReportHelpers.GetReportFolder(), "Videos");
                contextOptions.RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 };
            }

            return await browser.NewContextAsync(contextOptions);
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
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        }
    }
}
