using AventStack.ExtentReports;
using Microsoft.Playwright;
using PlaywrightWebTemplate.Pages;

namespace PlaywrightWebTemplate.Steps
{
    internal class LoginSteps(IPage page, ExtentTest test) 
    {
        public async Task DoLogin(string user, string password)
        {
            var loginPage = new LoginPage(page, test);

            // Actions
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

        }
    }
}
