using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightWebTemplate.Helpers
{
    public static class ExtentReportHelpers
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter;
        private static AsyncLocal<ExtentTest> _currentTest = new AsyncLocal<ExtentTest>();
        private static string _reportName = JsonHelpers.GetParameterAppSettings("REPORT_NAME") + 
                                            DateTime.Now.ToString("MM-dd-yyyy_HH-mm");
        private static string _reportFolderPath;
        private static string _environment = JsonHelpers.GetEnvironment("ENVIRONMENT");
        private static string _userName = Environment.UserName;
        private static string _reportDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        private static string _documentTitle = "Automated Tests Report";

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                var baseDir = Path.Combine(GeneralHelpers.GetProjectPath(), "Reports");
                _reportFolderPath = Path.Combine(baseDir, _reportName);
                Directory.CreateDirectory(_reportFolderPath); 

                var reportPath = Path.Combine(_reportFolderPath, _reportName + ".html");

                _sparkReporter = new ExtentSparkReporter(reportPath);
                _sparkReporter.Config.DocumentTitle = _documentTitle;
                _sparkReporter.Config.ReportName = _reportName;
                _sparkReporter.Config.Theme = Theme.Standard;

                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
                _extent.AddSystemInfo("Environment", _environment);
                _extent.AddSystemInfo("Executor", _userName);
                _extent.AddSystemInfo("Date", _reportDate);
            }

            return _extent;
        }

        public static void SetCurrentTest(ExtentTest test)
        {
            _currentTest.Value = test;
        }

        public static async Task AddTestResult(ExtentTest test, TestStatus status, string message, IPage page)
        {
            switch (status)
            {
                case TestStatus.Passed:
                    test.Pass("✅ ExtentTest Pass.");
                    break;

                case TestStatus.Failed:
                    test.Fail($"❌ ExtentTest Fail: {message}");

                    var screenshotDir = Path.Combine(_reportFolderPath, "ScreenShot");
                    Directory.CreateDirectory(screenshotDir);

                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                    Guid guid = Guid.NewGuid();
                    var fileName = $"error_{timestamp}_{guid}.png";
                    var screenshotPath = Path.Combine(screenshotDir, fileName);

                    await page.ScreenshotAsync(new PageScreenshotOptions
                    {
                        Path = screenshotPath,
                        FullPage = true
                    });

                    test.AddScreenCaptureFromPath(Path.Combine("ScreenShot", fileName));
                    break;

                case TestStatus.Skipped:
                    test.Skip("⚠️ ExtentTest skiped.");
                    break;

                default:
                    test.Warning("Unknow.");
                    break;
            }
        }

        public static async Task AttachExecutionVideoAsync(ExtentTest test, IPage page)
        {
            if (JsonHelpers.GetParameterAppSettings("RECORD_VIDEO")?.ToLower() != "true")
                return;

            if (page?.Video == null)
                return;

            try
            {
                var videoPath = await page.Video.PathAsync();
                var videoFileName = Path.GetFileName(videoPath);
                var reportFolder = GetReportFolder();
                var videoTargetFolder = Path.Combine(reportFolder, "Videos");
                var fullVideoPath = Path.Combine(videoTargetFolder, videoFileName);
                var relativeVideoPath = Path.Combine("Videos", videoFileName);

                if (!File.Exists(fullVideoPath))
                {
                    File.Copy(videoPath, fullVideoPath, overwrite: true);
                }
                test.Info($"📽️ <a href='{relativeVideoPath}' target='_blank'>Click to watch execution video</a>");
            }
            catch (Exception ex)
            {
                test.Warning($"⚠️ Unable to attach video: {ex.Message}");
            }
        }

        public static async Task LogStepWithScreenshotAsync(ExtentTest test, IPage page, string message, string fileNameBase)
        {
            if (JsonHelpers.GetParameterAppSettings("SCREENSHOT_EVERY_STEP") == "false")
            {
                test.Info(message);
                return;
            }

            var screenshotDir = Path.Combine(_reportFolderPath, "ScreenShot");
            Directory.CreateDirectory(screenshotDir);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            Guid guid = Guid.NewGuid();
            var fileName = $"{fileNameBase}_{DateTime.Now:yyyyMMdd_HHmmssfff}_{guid}.png";
            var fullPath = Path.Combine(screenshotDir, fileName);
            var relativePath = Path.Combine("ScreenShot", fileName);

            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = fullPath,
                FullPage = true
            });

            var media = MediaEntityBuilder.CreateScreenCaptureFromPath(relativePath).Build();
            test.Info(message, media);
        }

        public static string GetReportFolder()
        {
            return _reportFolderPath;
        }

        public static void Flush()
        {
            _extent?.Flush();
        }
    }
}
