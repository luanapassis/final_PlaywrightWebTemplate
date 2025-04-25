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
    internal class WhereToStartPage(IPage page, ExtentTest test) : PageBase(page, test)
    {
        //locators
        private ILocator httmlField => Page.GetByRole(AriaRole.Link, new() { Name = "Learn HTML" });

        //actions
        public async Task GoTo()
        {
            await Page.GotoAsync("https://www.w3schools.com/where_to_start.asp");
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        }

        public async Task ClickOnHtml()
        {
            await ClickAsync(httmlField);
        }
    }
}
