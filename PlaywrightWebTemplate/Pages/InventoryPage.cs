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
    internal class InventoryPage(IPage page, ExtentTest test) : PageBase(page, test)
    {

        //locators
        private ILocator productField => Page.Locator("[class='title']");

        //actions
        public async Task<string> GetProductsText()
        {
            return await GetTextAsync(productField);
        }
    }
}
