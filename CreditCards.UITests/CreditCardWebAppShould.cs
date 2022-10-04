using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using Xunit;
namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        public const string HomePageUrl = "http://localhost:44108/";
        public const string AboutPageUrl = "http://localhost:44108/Home/About";
        public const string HomePageTitle = "Home Page - Credit Cards";

        [Fact]
        [Trait("Categoty", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
              
                DemoHelper.Pause();               

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Categoty", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                driver.Navigate().Refresh();
                DemoHelper.Pause();

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Categoty", "Smoke")]
        public void ReloadHomePageBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutPageUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();
                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Categoty", "Smoke")]
        public void ReloadHomePageForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(AboutPageUrl);
                DemoHelper.Pause();
                driver.Navigate().Back();
                DemoHelper.Pause();

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
            }
        }
    }
}
