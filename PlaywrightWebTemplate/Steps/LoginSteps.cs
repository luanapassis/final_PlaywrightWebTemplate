using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using Azure;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;

namespace PlaywrightWebTemplate.Steps
{
    internal class LoginSteps : TestBase
    {
        public async Task DoLogin(string user, string password)
        {
            var loginPage = new LoginPage(Page, _test);

            // Actions
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

        }
    }
}
