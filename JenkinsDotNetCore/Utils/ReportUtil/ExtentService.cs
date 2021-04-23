using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.IO;

namespace JenkinsDotNetCore.Utils.ReportUtil
{
    public class ExtentService
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentService()
        {
            string reportDir = Path.Combine(ConfigUtil.GetProjectRootDirectory(), "Report");
            if (!Directory.Exists(reportDir))
            {
                Directory.CreateDirectory(reportDir);
            }
            var path = Path.Combine(reportDir, "index.html");
            var reporter = new ExtentHtmlReporter(Path.GetFullPath(path));
            reporter.Config.DocumentTitle = "JCMS Framework Report";
            reporter.Config.ReportName = "Test Execution Report";
            reporter.Config.Encoding = "UTF-8";
            reporter.Config.Theme = Theme.Standard;
            Instance.AttachReporter(reporter);
        }

        private ExtentService()
        {
        }
    }
}
