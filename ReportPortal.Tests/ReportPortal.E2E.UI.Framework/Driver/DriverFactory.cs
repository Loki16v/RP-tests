using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace ReportPortal.E2E.UI.Framework.Driver
{
    public static class DriverFactory
    {
        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            return _driver;
        }

        public static void CloseDriver()
        {
            _driver.Quit();
            _driver = null;
        }

        public static IWebDriver InitDriver(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    _driver = GetChromeInstanceWithOptions();
                    return _driver;
                case "Edge":
                    _driver = GetEdgeInstanceWithOptions();
                    return _driver;
                default:
                    throw new NotSupportedException($"BrowserType {browser} is not supported.");
            }
        }

        private static ChromeDriver GetChromeInstanceWithOptions()
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            return driver;
        }

        private static EdgeDriver GetEdgeInstanceWithOptions()
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            return driver;
        }
    }
}
