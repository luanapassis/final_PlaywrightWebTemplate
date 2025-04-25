using Azure;
using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;

namespace PlaywrightWebTemplate.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class WorkingWirhTabsTests : TestBase
    {
        [Test]
        public async Task TestWorkingWithTabs()
        {
            WhereToStartPage whereToStartPage = new WhereToStartPage(Page, ExtentTest);
            HtmlIntroPage htmlIntroPage = new HtmlIntroPage(Page, ExtentTest);
            TryHtmlIntroPage tryHtmlIntroPage = new TryHtmlIntroPage(Page, ExtentTest);

            await whereToStartPage.GoTo();
            await whereToStartPage.ClickOnHtml();
            await htmlIntroPage.ClickOnTryYourself();
            await tryHtmlIntroPage.GoingToLastTabOpened();
            await tryHtmlIntroPage.ClickInMenu();
            await tryHtmlIntroPage.ChangeOrientation();
            await htmlIntroPage.TurnToTheFirst();
            bool isTextPresent = await htmlIntroPage.IsTextPresent();
            Assert.IsTrue(isTextPresent, "The text 'What is an HTML Element?' was not found on the page.");
        }

        [Test]
        public async Task TestWorkingWithTabsAndClosing()
        {
            WhereToStartPage whereToStartPage = new WhereToStartPage(Page, ExtentTest);
            HtmlIntroPage htmlIntroPage = new HtmlIntroPage(Page, ExtentTest);
            TryHtmlIntroPage tryHtmlIntroPage = new TryHtmlIntroPage(Page, ExtentTest);

            await whereToStartPage.GoTo();
            await whereToStartPage.ClickOnHtml();
            await htmlIntroPage.ClickOnTryYourself();
            await tryHtmlIntroPage.GoingToLastTabOpened();
            await tryHtmlIntroPage.ClickInMenu();
            await tryHtmlIntroPage.ChangeOrientation();
            await tryHtmlIntroPage.CloseTab();
            bool isTextPresent = await htmlIntroPage.IsTextPresent();
            Assert.IsTrue(isTextPresent, "The text 'What is an HTML Element?' was not found on the page.");
        }


        [Test]
        public async Task TestOtherSite()
        {
            DemoQAPage demoQAPage = new DemoQAPage(Page, ExtentTest);

            await demoQAPage.GoTo();
            await demoQAPage.ClickInNewTab();
            await demoQAPage.SwitchToLastTabOpened();
            bool isTextPresent = await demoQAPage.IsTextPresent();
            Assert.IsTrue(isTextPresent);            
            await demoQAPage.SwitchToFirstTabOpened();
            await demoQAPage.ClickInNewTab();
            await demoQAPage.SwitchToLastTabOpened();
            await demoQAPage.SwitchToFirstTabOpened();

        }

        [Test]
        public async Task TestOtherSite2()
        {
            DemoQAPage demoQAPage = new DemoQAPage(Page, ExtentTest);

            await demoQAPage.GoTo();
            string url = await demoQAPage.GetUrl();
            Assert.That(url, Is.EqualTo("https://demoqa.com/browser-windows"));
            await demoQAPage.ClickInNewTab();
            await demoQAPage.SwitchToLastTabOpened();
            bool isTextPresent = await demoQAPage.IsTextPresent();
            Assert.IsTrue(isTextPresent);
            await demoQAPage.CloseTab();
            
            await demoQAPage.ClickInNewWindows();
            await demoQAPage.SwitchToLastTabOpened();
            bool isText2Present = await demoQAPage.IsTextPresent();
            Assert.IsTrue(isText2Present);
            string url2 = await demoQAPage.GetUrl();
            Assert.That(url2, Is.EqualTo("https://demoqa.com/sample")); // Replace with the expected URL
            await demoQAPage.CloseTab();


            demoQAPage.ClickInNewPop();
            await demoQAPage.SwitchToLastTabOpened();
            string url3 = await demoQAPage.GetUrl();
            bool isText3Present = await demoQAPage.IsTextPresentPop();
            await demoQAPage.CloseTab();
            string url4 = await demoQAPage.GetUrl();
            Assert.That(url4, Is.EqualTo("https://demoqa.com/browser-windows"));

        }
    }
}
