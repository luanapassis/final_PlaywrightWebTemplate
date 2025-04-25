using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;

namespace PlaywrightWebTemplate.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class TwoWindowsTests : TestBase
    {
        [Test]
        public async Task TestTwoWindows()
        {
            WhereToStartPage whereToStartPage = new WhereToStartPage(Page, ExtentTest);
            HtmlIntroPage htmlIntroPage = new HtmlIntroPage(Page, ExtentTest);
            TryHmlIntroPage tryHmlIntroPage = new TryHmlIntroPage(Page, ExtentTest);

            await whereToStartPage.GoTo();
            await whereToStartPage.ClickOnHtml();
            await htmlIntroPage.ClickOnTryYourself();
            await tryHmlIntroPage.GoingToLastTabOpened();
            await tryHmlIntroPage.ClickInMenu();
            await tryHmlIntroPage.ChangeOrientation();
            await htmlIntroPage.TurnToTheFirst();
            bool isTextPresent = await htmlIntroPage.IsTextPresent();
            Assert.IsTrue(isTextPresent, "The text 'What is an HTML Element?' was not found on the page.");
        }
    }
}
