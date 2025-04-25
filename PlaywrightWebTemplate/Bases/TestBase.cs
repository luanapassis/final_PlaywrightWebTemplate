using AventStack.ExtentReports;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightWebTemplate.Helpers;
using System.Threading.Tasks;

namespace PlaywrightWebTemplate.Bases
{
    public abstract class TestBase
    {
        protected IPlaywright PlaywrightInstance;
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPage Page;
        protected ExtentTest ExtentTest;

        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            PlaywrightInstance = await Playwright.CreateAsync();
            Browser = await BrowserHelpers.CreateBrowserAsync(PlaywrightInstance);
        }

        [SetUp]
        public async Task SetupAsync()
        {
            ExtentTest = ExtentReportHelpers.GetExtent().CreateTest(TestContext.CurrentContext.Test.Name);
            ExtentReportHelpers.SetCurrentTest(ExtentTest);
            Context = await PlaywrightSessionHelpers.CreateContextAsync(Browser);
            Page = await PlaywrightSessionHelpers.CreatePageAsync(Context);
            await PlaywrightSessionHelpers.NavigateToInitialPageAsync(Page);
        }             

        [TearDown]
        public async Task TearDownAsync()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            await ExtentReportHelpers.AttachAllExecutionVideosAsync(ExtentTest, Context);
            await ExtentReportHelpers.AddTestResult(ExtentTest, status, message, Page);

            await Context.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync()
        {
            await Browser.CloseAsync();
            PlaywrightInstance.Dispose();
            ExtentReportHelpers.Flush();
        }
    }
}
