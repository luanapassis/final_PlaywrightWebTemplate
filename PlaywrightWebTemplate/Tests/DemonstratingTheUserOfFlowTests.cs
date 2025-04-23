using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;
using PlaywrightWebTemplate.Steps;

namespace PlaywrightWebTemplate.Tests
{
    internal class DemonstratingTheUserOFFlowTests : TestBase
    {

        [Test]
        public async Task DoLoginSuccessfullyWithStep()
        {
            // Arrange
            string user = "standard_user";
            string password = "secret_sauce";
            var loginSteps = new LoginSteps();
            var inventoryPage = new InventoryPage(Page, _test); 

            // Actions
            await loginSteps.DoLogin(user, password);

            // Assertions
            string actualText = await inventoryPage.GetProductsText(); // Ensure GetProductsText() is awaited
            Assert.That(actualText, Is.EqualTo("Products"));
        }
    
    }
}
