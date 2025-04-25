using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;

namespace PlaywrightWebTemplate.Tests
{
    
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class AnotherLoginTests : TestBase
    {
        [Test]
        public async Task TestingSomethingWillPass()
        {
            //arrange
            string user = "tst 1";
            string password = "SuperSecretPassword!";
            var loginPage = new LoginPage(Page, ExtentTest); 

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
            string user = "wrong 2";
            string password = "WrongPass";
            var loginPage = new LoginPage(Page, ExtentTest);

            //actions
            await loginPage.TypeUserName(user);
            await loginPage.TypePass(password);
            await loginPage.ClickEnter();

            //assertions
            Assert.True(false);

        }
    }
}