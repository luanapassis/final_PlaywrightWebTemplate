using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightWebTemplate.Bases;

namespace PlaywrightWebTemplate.Pages
{
    internal class DemoQAPage(IPage page, ExtentTest test) : PageBase(page, test)
    {
        //locators
        private ILocator newTabButton => Page.Locator("//*[@id='tabButton']");
        private ILocator windowsButton => Page.Locator("//*[@id='windowButton']");
        private ILocator newTabText => Page.Locator("h1", new() { HasText = "This is a sample page" });
        
        private ILocator newPopButton => Page.Locator("//*[@id='messageWindowButton']");
        private ILocator newPopText => Page.Locator("body", new() { HasText = "Knowledge increases by sharing but not by saving" });
        //actions
        public async Task GoTo()
        {
            await Page.GotoAsync("https://demoqa.com/browser-windows");
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task ClickInNewTab()
        {
            await ClickAsync(newTabButton);
        }
        public async Task ClickInNewWindows()
        {
            await ClickAsync(windowsButton);
        }
        public async Task CloseTab()
        {
            await CloseCurrentTabAsync();
        }
        public async Task<bool> IsTextPresent()
        {
            return await GetIfElementExists(newTabText);
        }
        public async Task SwitchToLastTabOpened()
        {
            await SwitchToLastTabAsync();
        }
        public async Task SwitchToFirstTabOpened()
        {
            await SwitchToFirstTabAsync();
        }

        public async Task CloseCurrentTab()
        {
            await CloseCurrentTabAsync();
        }

        public async Task<string> GetUrl()
        {
            return GetCurrentUrl();
        }

        public async Task<bool> IsTextPresentPop()
        {
            return await GetIfElementExists(newPopText);
        }

        public async Task ClickInNewPop()
        {
            await ClickAsync(newPopButton);
        }
    }
}
