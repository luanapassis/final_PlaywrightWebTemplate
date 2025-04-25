using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;
using PlaywrightWebTemplate.Steps;

namespace PlaywrightWebTemplate.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class UsingStepsTests : TestBase
    {
        [Test]
        public async Task DoLoginSuccessfullyWithStep()
        {
            // Arrange
            string user = "standard_user";
            string password = "secret_sauce";
            var loginSteps = new LoginSteps(Page, ExtentTest);
            var inventoryPage = new InventoryPage(Page, ExtentTest); 

            // Actions
            await loginSteps.DoLogin(user, password);

            // Assertions
            string actualText = await inventoryPage.GetProductsText(); // Ensure GetProductsText() is awaited
            Assert.That(actualText, Is.EqualTo("Products"));
        }
    
    }
}
