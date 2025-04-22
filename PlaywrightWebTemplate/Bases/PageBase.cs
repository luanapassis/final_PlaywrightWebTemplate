using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightWebTemplate.Helpers;

namespace PlaywrightWebTemplate.Bases
{
    public abstract class PageBase
    {
        protected readonly IPage Page;
        protected readonly ExtentTest Test;


        protected PageBase(IPage page, ExtentTest test)
        {
            Page = page;
            Test = test;

        }



        #region Interactions
        protected async Task ClickAsync(ILocator locator)
        {
            await locator.ClickAsync();

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"🖱️ Clicked on **{locator}**",
                "Click"
            );
        }

        protected async Task ClickAsyncWithRetry(ILocator locator, int timeoutInSeconds = 30)
        {
            var timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            var start = DateTime.Now;

            Exception? lastException = null;

            while ((DateTime.Now - start) < timeout)
            {
                try
                {
                    await locator.ClickAsync(new LocatorClickOptions
                    {
                        Timeout = 3000 // espera curta por tentativa
                    });

                    await ExtentReportHelpers.LogStepWithScreenshotAsync(
                        Test,
                        Page,
                        $"🖱️ Clicking on **{locator}** with retry",
                        "ClickWithRetry"
                    );

                    return;
                }
                catch (PlaywrightException ex)
                {
                    lastException = ex;

                    if (!ex.Message.Contains("Another element would receive the click") &&
                        !ex.Message.Contains("Element is not attached") &&
                        !ex.Message.Contains("not visible"))
                    {
                        throw;
                    }

                    // Espera pequena antes de tentar novamente
                    await Task.Delay(500);
                }
            }

            throw new TimeoutException($"❌ Unable to click on element: {locator} within {timeoutInSeconds}s. Last error: {lastException?.Message}");
        }


        protected async Task SendKeysAsync(ILocator locator, string text)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await locator.FillAsync(text);
            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"📝 Typing **{text}** into **{locator}**",
                "SendKeys"
            );
        }

        protected async Task<string> GetTextAsync(ILocator locator)
        {
            var text = await locator.InnerTextAsync();
            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"🔍 Retrieved text from **{locator}**: `{text}`",
                "GetText"
            );
            return text;
        }

        protected async Task<bool> ElementExistsAsync(ILocator locator)
        {
            try
            {
                float timeout;
                if (!float.TryParse(JsonHelpers.GetParameterAppSettings("DEFAULT_TIMEOUT"), out timeout))
                {
                    timeout = 30000; // Default timeout in milliseconds if parsing fails
                }

                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = timeout
                });
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        protected async Task OpenNewTabAsync()
        {
            var newPage = await Page.Context.NewPageAsync();
            await newPage.BringToFrontAsync();

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                newPage,
                $"🆕 PARAMETER: Opened a new tab",
                "OpenNewTab"
            );
        }

        protected async Task SwitchToFirstTabAsync()
        {
            var firstPage = Page.Context.Pages.First();
            await firstPage.BringToFrontAsync();

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                firstPage,
                $"📑 PARAMETER: Switched to first tab",
                "SwitchToFirstTab"
            );
        }

        protected async Task SwitchToLastTabAsync()
        {
            var lastPage = Page.Context.Pages.Last();
            await lastPage.BringToFrontAsync();

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                lastPage,
                $"📑 PARAMETER: Switched to last tab",
                "SwitchToLastTab"
            );
        }
        #endregion
    }
}
