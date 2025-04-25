using AventStack.ExtentReports;
using Azure;
using Microsoft.Playwright;
using PlaywrightWebTemplate.Helpers;

namespace PlaywrightWebTemplate.Bases
{
    public abstract class PageBase
    {
        protected IPage Page;
        protected ExtentTest Test;

        protected PageBase(IPage page, ExtentTest test)
        {
            Page = page;
            Test = test;
        }

        #region Interactions
        protected async Task ClickAsync(ILocator locator)
        {
            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"🖱️ Clicking on **{locator}**",
                "Click"
            );

            await locator.ClickAsync();

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"🖱️ After Clicking on **{locator}**",
                "AfterClick"
            );
        }

        protected async Task SendKeysAsync(ILocator locator, string text)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await locator.FillAsync(text);
            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"📝 Typing **{text}** into **{locator}**",
                "Fill"
            );
        }

        protected async Task<string> GetTextAsync(ILocator locator)
        {
            var text = await locator.InnerTextAsync();
            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"🔍 Getting text from **{locator}**: `{text}`",
                "InnerText"
            );
            return text;
        }

        protected async Task<bool> GetIfElementExists(ILocator locator)
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
                
                await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                Page,
                $"🔍 Verifying if element exists **{locator}**",
                "VerifyIfElementExist"
            );
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
            await newPage.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await newPage.BringToFrontAsync();

            Page = newPage;

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                newPage,
                $"🆕 Opening a new tab",
                "OpenNewTab"
            );
        }

        protected async Task SwitchToFirstTabAsync()
        {
            var firstPage = Page.Context.Pages.First();

            await firstPage.BringToFrontAsync();

            Page = firstPage;

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                firstPage,
                $"📑 Switching to first tab",
                "SwitchToFirstTab"
            );
        }

        protected async Task SwitchToLastTabAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            var lastPage = Page.Context.Pages.Last();            

            await lastPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await lastPage.BringToFrontAsync();

            Page = lastPage;

            await ExtentReportHelpers.LogStepWithScreenshotAsync(
                Test,
                lastPage,
                $"📑 Switching to last tab",
                "SwitchToLastTab"
            );
        }
        #endregion
    }
}
