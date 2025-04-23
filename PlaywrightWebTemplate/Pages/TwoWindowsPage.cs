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
    internal class TwoWindowsPage(IPage page, ExtentTest test) : PageBase(page, test)
    {

        //locators
        private ILocator httmlField => Page.GetByRole(AriaRole.Link, new() { Name = "Learn HTML" });
        private ILocator tryYourselfField => Page.GetByText("Try it Yourself »");
        private ILocator menuButton => Page.Locator("//*[@id='menuButton']");
        private ILocator tabText => Page.GetByText("What is an HTML Element?\r\n");

        //actions
        public async Task GoTo()
        {
            await Page.GotoAsync("https://www.w3schools.com/where_to_start.asp");
        }

        public async Task ClickOnHtml()
        {
            await ClickAsyncWithRetry(httmlField);
        }
        public async Task ClickOnTryYourself()
        {
            await ClickAsyncWithRetry(tryYourselfField);
        }

        public async Task GoingToLastTabOpened()
        {
            await SwitchToLastTabAsync();
        }

        public async Task ClickInMenu()
        {
            await ClickAsync(menuButton);
        }

        public async Task IsGoingToOpenNewTab()
        {
            await OpenNewTabAsync();
        }

        public async Task TurnToTheFirst()
        {
            await SwitchToFirstTabAsync();
        }

        public async Task<bool> IsTextPresent()
        {
            return await GetIfElementExists(tabText);
        }
    }
}
