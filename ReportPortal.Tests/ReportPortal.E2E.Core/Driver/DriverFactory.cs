using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using ReportPortal.E2E.Core.Models;

namespace ReportPortal.E2E.Core.Driver
{
    public class DriverFactory
    {
        private static readonly RemoteRunOptionsModel RemoteRunOptions =
            TestsBootstrap.Instance.ServiceProvider.GetService<RemoteRunOptionsModel>();
        private static readonly int DefaultTimeOut =
            TestsBootstrap.Instance.Configuration.GetSection("ImplicitWaitTimeOut").Get<int>();
        private static readonly int PageLoadTimeOut =
            TestsBootstrap.Instance.Configuration.GetSection("PageLoadTimeOut").Get<int>();

        private IWebDriver _driver;

        public DriverFactory(string browser, bool isRemote = false, string remoteRunName = null)
        {
            Enum.TryParse(browser, out Browser browserName);
            if (isRemote)
            {
                var options = new Dictionary<string, object>();
                if (remoteRunName != null) options.Add("name", remoteRunName);
                options.Add("username", RemoteRunOptions.UserName);
                options.Add("accessKey", RemoteRunOptions.AccessKey);
                options.Add("platformName", RemoteRunOptions.PlatformName);
                options.Add("build", "ReportPortal.E2E.UI.Tests");
                options.Add("project", "TestRun");
                options.Add("w3c", true);
                options.Add("plugin", "c#-nunit");

                _driver = browserName switch
                {
                    Browser.Chrome => GetChromeRemoteDriver(options),
                    Browser.Edge => GetEdgeRemoteDriver(options),
                    _ => throw new NotSupportedException($"BrowserType {browser} is not supported.")
                };
            }
            else
            {
                _driver = browserName switch
                {
                    Browser.Chrome => GetChromeInstanceWithOptions(),
                    Browser.Edge => GetEdgeInstanceWithOptions(),
                    _ => throw new NotSupportedException($"BrowserType {browser} is not supported.")
                };
            }
        }
        public IWebDriver GetDriver()
        {
            return _driver;
        }

        public void CloseDriver()
        {
            _driver.Quit();
            _driver = null;
        }

        #region Private Methods

        private ChromeDriver GetChromeInstanceWithOptions()
        {
            var service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            service.LogPath = $"{AppDomain.CurrentDomain.BaseDirectory}ChromeDriver.log";

            var options = new ChromeOptions();
            options.AddArguments("--disable-web-security", "start-maximized", "--disable-extensions");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", false);

            options.AddArgument("--safebrowsing-disable-download-protection");
            options.AddArgument("--safebrowsing-disable-extension-blacklist");
            options.AddArgument("--safebrowsing-disable-auto-update");
            options.AddArgument("--safebrowsing-manual-download-blacklist");
            options.AddArgument("--allow-unchecked-dangerous-downloads");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-running-insecure-content");
            options.AddArgument("--allow-insecure-localhost");
            options.AddArgument("--disable-popup-blocking");

            var driver = new ChromeDriver(service, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(DefaultTimeOut);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PageLoadTimeOut);
            return driver;
        }

        private EdgeDriver GetEdgeInstanceWithOptions()
        {
            var service = EdgeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            service.LogPath = $"{AppDomain.CurrentDomain.BaseDirectory}EdgeDriver.log";

            var options = new EdgeOptions();
            options.AddArguments("--disable-web-security", "start-maximized", "--disable-extensions");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("disable-popup-blocking", true);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", false);

            options.AddArgument("--safebrowsing-disable-download-protection");
            options.AddArgument("--safebrowsing-disable-extension-blacklist");
            options.AddArgument("--safebrowsing-disable-auto-update");
            options.AddArgument("--safebrowsing-manual-download-blacklist");
            options.AddArgument("--allow-unchecked-dangerous-downloads");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--allow-running-insecure-content");
            options.AddArgument("--allow-insecure-localhost");
            options.AddArgument("--disable-popup-blocking");

            var driver = new EdgeDriver(service, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(DefaultTimeOut); 
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PageLoadTimeOut);
            return driver;
        }

        private RemoteWebDriver GetChromeRemoteDriver(Dictionary<string, object> options)
        {

            var capabilities = new ChromeOptions
            {
                BrowserVersion = RemoteRunOptions.ChromeVersion
            };
            capabilities.AddAdditionalOption("LT:Options", options);

            var driver = new RemoteWebDriver(new Uri(RemoteRunOptions.RemoteDriverUrl), capabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(DefaultTimeOut);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PageLoadTimeOut);
            return driver;
        }

        private RemoteWebDriver GetEdgeRemoteDriver(Dictionary<string, object> options)
        {

            var capabilities = new EdgeOptions
            {
                BrowserVersion = RemoteRunOptions.EdgeVersion
            };
            capabilities.AddAdditionalOption("LT:Options", options);

            var driver = new RemoteWebDriver(new Uri(RemoteRunOptions.RemoteDriverUrl), capabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(DefaultTimeOut);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(PageLoadTimeOut);
            return driver;
        }

        #endregion
    }
}
