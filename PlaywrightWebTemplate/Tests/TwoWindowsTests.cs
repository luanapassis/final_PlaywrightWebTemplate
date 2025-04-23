using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightWebTemplate.Bases;
using PlaywrightWebTemplate.Pages;

namespace PlaywrightWebTemplate.Tests
{
    [TestFixture]
    internal class TwoWindowsTests : TestBase
    {
        [Test]
        public async Task TestTwoWindows()
        {          

            TwoWindowsPage twoWindowsPage = new TwoWindowsPage(Page, _test);
     

            await twoWindowsPage.GoTo();
            await twoWindowsPage.ClickOnHtml();
            await twoWindowsPage.ClickOnTryYourself();
            await twoWindowsPage.GoingToLastTabOpened();
            await twoWindowsPage.ClickInMenu();
            await twoWindowsPage.TurnToTheFirst();
            bool isTextPresent = await twoWindowsPage.IsTextPresent();
            Assert.IsTrue(isTextPresent, "The text 'What is an HTML Element?' was not found on the page.");
        }
    }
}
