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
    internal class HtmlIntroPage(IPage page, ExtentTest test) : PageBase(page, test)
    {
        //locators
        private ILocator tryYourselfField => Page.GetByText("Try it Yourself »");
        private ILocator tabText => Page.GetByText("What is an HTML Element?\r\n");


        //actions
        public async Task ClickOnTryYourself()
        {
            await ClickAsync(tryYourselfField);
        }
        public async Task<bool> IsTextPresent()
        {
            return await GetIfElementExists(tabText);
        }
        public async Task TurnToTheFirst()
        {
            await SwitchToFirstTabAsync();
        }

    }
}
