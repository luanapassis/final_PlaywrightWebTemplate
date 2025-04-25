using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;

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
            var loginPage = new LoginPage(Page, ExtentTest);
            var inventoryPage = new InventoryPage(Page, ExtentTest); // ExtentTest is defined in Setup()

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
            string user = "wrong 1";
            string password = "WrongPass";
            var loginPage = new LoginPage(Page, ExtentTest); // ExtentTest é definido no Setup()

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