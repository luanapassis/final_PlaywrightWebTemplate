using AventStack.ExtentReports;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PlaywrightWebTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightWebTemplate.Bases
{
    public abstract class TestBaseBKP : PageTest
    {
        private ExtentReports _extent;
        protected ExtentTest _test;


        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ColorScheme = ColorScheme.Light,
                ViewportSize = new()
                {
                    Width = 1920,
                    Height = 1080
                },
                BaseURL = JsonHelpers.GetParameterAppSettings("URL"),
            };
        }


        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _extent = ExtentReportHelpers.GetExtent();

        }

        [SetUp]
        public async Task Setup()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            float defaultTimeout = float.Parse(JsonHelpers.GetParameterAppSettings("DEFAULT_TIMEOUT"));
            Context.SetDefaultTimeout(defaultTimeout); 
            Context.SetDefaultNavigationTimeout(defaultTimeout); 

            await Page.GotoAsync("");
        }

        [TearDown]
        public async Task TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            await ExtentReportHelpers.RegistrarResultadoAsync(_test, status, message, Page);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            ExtentReportHelpers.Flush();
        }
    }
}
