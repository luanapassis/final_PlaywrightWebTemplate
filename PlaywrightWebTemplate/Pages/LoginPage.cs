using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightWebTemplate.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightWebTemplate.Pages
{
    internal class LoginPage(IPage page, ExtentTest test) : PageBase(page, test)
    {

        //locators
        private ILocator usernameField => Page.Locator("[id='user-name']");
        private ILocator passField => Page.Locator("[id='password']");
        private ILocator loginButton => Page.Locator("[type='submit']");
        private ILocator errorMessage => Page.GetByText("Epic sadface: Username and password do not match any user in this service");

        //actions
        public async Task TypeUserName(string user)
        {
            await SendKeysAsync(usernameField, user);
        }

        public async Task TypePass(string pass)
        {
            await SendKeysAsync(passField, pass);
        }

        public async Task ClickEnter()
        {
            await ClickAsync(loginButton);
        }

        public async Task<bool> GetErrorMessage()
        {
            return await GetIfElementExists(errorMessage);
        }
    }
}
