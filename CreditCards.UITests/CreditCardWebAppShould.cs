using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using Xunit;

namespace CreditCards.UITests
{
    [Trait("Categoty", "Smoke")]
    public class CreditCardWebAppShould
    {
        public const string HomePageUrl = "http://localhost:44108/";
        public const string AboutPageUrl = "http://localhost:44108/Home/About";
        public const string HomePageTitle = "Home Page - Credit Cards";

        [Fact]       
        public void LoadApplicationPageWithSizeAndPosition()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
              
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();
                driver.Manage().Window.Minimize();
                DemoHelper.Pause();
                driver.Manage().Window.Size = new System.Drawing.Size(500, 500);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(500, 500);
                DemoHelper.Pause();
                driver.Manage().Window.FullScreen();
                DemoHelper.Pause(5000);

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
            }
        }

        [Fact]       
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                var initialGenerationToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().Refresh();
                DemoHelper.Pause();
                var refreshGenerationToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
                Assert.NotEqual(initialGenerationToken, refreshGenerationToken);
            }
        }

        [Fact]
        public void ReloadHomePageBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause(); 
                var initialGenerationToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().GoToUrl(AboutPageUrl);               
                DemoHelper.Pause();               

                driver.Navigate().Forward();
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();
                var refreshGenerationToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
                Assert.NotEqual(initialGenerationToken, refreshGenerationToken);
            }
        }

        [Fact]
        public void ReloadHomePageForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutPageUrl);
                DemoHelper.Pause();                

                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                var initialGenerationToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();
                var refreshGenerationToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.Equal(HomePageTitle, driver.Title);
                Assert.Equal(HomePageUrl, driver.Url);
                Assert.NotEqual(initialGenerationToken, refreshGenerationToken);
            }
        }

        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                var firstCellOftable = driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", firstCellOftable[0].Text);
                Assert.Equal("20% APR", firstCellOftable[1].Text);

                Assert.Equal("Silver Credit Card", firstCellOftable[2].Text);
                Assert.Equal("18% APR", firstCellOftable[3].Text);

                Assert.Equal("Gold Credit Card", firstCellOftable[4].Text);
                Assert.Equal("17% APR", firstCellOftable[5].Text);

                Assert.Equal(HomePageUrl, driver.Url);              
            }
        }

        [Fact]
        public void AlertIfLiveChartClose() 
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                driver.FindElement(By.Id("LiveChat")).Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                Assert.Equal("Live chat is currently closed.", alert.Text);
                DemoHelper.Pause();

                alert.Accept();
                DemoHelper.Pause();
            }
        }

        [Fact]
        public void LearnAbout_Ok()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                driver.FindElement(By.Id("LearnAboutUs")).Click();
                DemoHelper.Pause();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.Equal("Do you want to learn more about us?", alert.Text);
                DemoHelper.Pause();

                alert.Accept();
                DemoHelper.Pause();

                Assert.Equal(AboutPageUrl, driver.Url);
            }
        }

        [Fact]
        public void LearnAbout_Cancel()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                driver.FindElement(By.Id("LearnAboutUs")).Click();
                DemoHelper.Pause();

                IAlert alert = driver.SwitchTo().Alert();

                Assert.Equal("Do you want to learn more about us?", alert.Text);
                DemoHelper.Pause();

                alert.Dismiss();
                DemoHelper.Pause();

                Assert.Equal(HomePageTitle, driver.Title);
            }
        }

        [Fact]
        public void NotDisplayCookieUseMessage() 
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);

                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));

                driver.Navigate().Refresh();

                var message = driver.FindElements(By.Id("CookiesBeingUsed"));

                Assert.Empty(message);

                Cookie cookie = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                Assert.Equal("true", cookie.Value);

                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompare4Reporter))]
        public void RenderAboutPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutPageUrl);

                ITakesScreenshot screenshotAboutPageDriver = (ITakesScreenshot)driver;

                Screenshot screenshotAboutPage = screenshotAboutPageDriver.GetScreenshot();
                screenshotAboutPage.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("aboutpage.bmp");

                Approvals.Verify(file);
            }
        }
    }
}
