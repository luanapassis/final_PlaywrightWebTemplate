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
    internal class TryHtmlIntroPage(IPage page, ExtentTest test) : PageBase(page, test)
    {
        //locators
        private ILocator menuButton => Page.Locator("//*[@id='menuButton']");
        private ILocator changeOrientationMenu => Page.Locator("span", new() { HasText = "Change Orientation" });

        //actions
        
        public async Task GoingToLastTabOpened()
        {
            await SwitchToLastTabAsync();
        }
        public async Task ClickInMenu()
        {
            await ClickAsync(menuButton);
        }
        public async Task ChangeOrientation()
        {
            await ClickAsync(changeOrientationMenu);
        }

        public async Task CloseTab()
        {
            await CloseCurrentTabAsync();
        }

    }
}
