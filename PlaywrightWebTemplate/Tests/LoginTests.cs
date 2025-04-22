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
    public class LoginTests : TestBase
    {

        [Test]
        public async Task DoLoginSuccessfully()
        {
            // Arrange
            string user = "standard_user";
            string password = "secret_sauce";
            var loginPage = new LoginPage(Page, _test);
            var inventoryPage = new InventoryPage(Page, _test); // _test is defined in Setup()

            // Actions
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

            // Assertions
            string actualText = await inventoryPage.GetProductsText(); // Ensure GetProductsText() is awaited
            Assert.That(actualText, Is.EqualTo("Products"));
        }

        [Test]
        public async Task DoLoginWithWrongCredentials()
        {
            //arrange
            string user = "errado 1";
            string password = "WrongPass";
            var loginPage = new LoginPage(Page, _test); // _test é definido no Setup()

            //actions
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

            //assertions
            bool errorMessage = await loginPage.GetErrorMessage();
            Assert.True(errorMessage);

        }
    }
}