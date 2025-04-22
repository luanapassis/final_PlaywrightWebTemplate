using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;
using System.Text.RegularExpressions;

namespace PlaywrightWebTemplate.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LoginTests2 : TestBase
    {
        [Test]
        public async Task TestingSomethingWillPass()
        {
            //arrange
            string user = "certo 1";
            string password = "SuperSecretPassword!";
            var loginPage = new LoginPage(Page, _test); // _test é definido no Setup()

            //actions
            //await loginPage.GotoAsync();
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

            //assertions
            Assert.True(true);
        }

        [Test]
        public async Task TestingSomethingWillNotPass()
        {
            //arrange
            string user = "errado 2";
            string password = "WrongPass";
            var loginPage = new LoginPage(Page, _test); // _test é definido no Setup()

            //actions
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

            //assertions
            Assert.True(false);

        }
    }
}