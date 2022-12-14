namespace CreditCards.UITests
{
    using ApprovalTests;
    using ApprovalTests.Reporters;
    using ApprovalTests.Reporters.Windows;
    using CreditCards.UITests.ObjectModels;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using Xunit;

    [Trait("Categoty", "Smoke")]
    public class CreditCardWebAppShould : IClassFixture<ChromeDriverFixture>
    {
        private readonly ChromeDriverFixture chromeDriverFixture;

        public CreditCardWebAppShould(ChromeDriverFixture chromeDriverFixture)
        {
            this.chromeDriverFixture = chromeDriverFixture;
            this.chromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            this.chromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
        }

        [Fact]       
        public void LoadApplicationPageWithSizeAndPosition()
        {
            var homePageObjectModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageObjectModel.NavigateTo();

            chromeDriverFixture.Driver.Manage().Window.Maximize();
            DemoHelper.Pause();
            chromeDriverFixture.Driver.Manage().Window.Minimize();
            DemoHelper.Pause();
            chromeDriverFixture.Driver.Manage().Window.Size = new System.Drawing.Size(500, 500);
            DemoHelper.Pause();
            chromeDriverFixture.Driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
            DemoHelper.Pause();
            chromeDriverFixture.Driver.Manage().Window.Position = new System.Drawing.Point(500, 500);
            DemoHelper.Pause();
            chromeDriverFixture.Driver.Manage().Window.FullScreen();
            DemoHelper.Pause(5000); 
        }

        [Fact]
        public void LoadHomePage()
        {
            var homePageObjectModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageObjectModel.NavigateTo();         
        }

        [Fact]
        public void ReloadHomePageBack()
        {
            var homePageObjectModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageObjectModel.NavigateTo();

            var initialGenerationToken = homePageObjectModel.GetToken;

            var applicationPageObjectModel = new ApplicationPageObjectModel(chromeDriverFixture.Driver);
            applicationPageObjectModel.NavigateTo();

            chromeDriverFixture.Driver.Navigate().Back();

            homePageObjectModel.EnsurePageIsLoaded();
            var refreshGenerationToken = homePageObjectModel.GetToken;               
              
            Assert.NotEqual(initialGenerationToken, refreshGenerationToken);
        }

        [Fact]
        public void ReloadHomePageForward()
        {
            var applicationPageObjectModel = new ApplicationPageObjectModel(chromeDriverFixture.Driver);
            applicationPageObjectModel.NavigateTo();

            var homePageObjectModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageObjectModel.NavigateTo();

            var initialGenerationToken = homePageObjectModel.GetToken;

            chromeDriverFixture.Driver.Navigate().Back();
            chromeDriverFixture.Driver.Navigate().Forward();

            homePageObjectModel.EnsurePageIsLoaded();
            var refreshGenerationToken = homePageObjectModel.GetToken;

            Assert.NotEqual(initialGenerationToken, refreshGenerationToken);
        }

        [Fact]
        public void DisplayProductsAndRates()
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            Assert.Equal("Easy Credit Card", homePageModel.Products[0].productName);
            Assert.Equal("20% APR", homePageModel.Products[0].interestRate);

            Assert.Equal("Silver Credit Card", homePageModel.Products[1].productName);
            Assert.Equal("18% APR", homePageModel.Products[1].interestRate);

            Assert.Equal("Gold Credit Card", homePageModel.Products[2].productName);
            Assert.Equal("17% APR", homePageModel.Products[2].interestRate);
        }

        [Fact]
        public void AlertIfLiveChartClose() 
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            homePageModel.LiveChatClick();

            WebDriverWait wait = new WebDriverWait(chromeDriverFixture.Driver, TimeSpan.FromSeconds(5));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

            Assert.Equal("Live chat is currently closed.", alert.Text);

            alert.Accept();
        }

        [Fact]
        public void LearnAboutClick_Ok()
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            homePageModel.LearnAboutUsClick();

            WebDriverWait wait = new WebDriverWait(chromeDriverFixture.Driver, TimeSpan.FromSeconds(5));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());              

            Assert.Equal("Do you want to learn more about us?", alert.Text);

            alert.Accept();

            Assert.Equal(HomePageObjectModel.AboutPageUrl, chromeDriverFixture.Driver.Url);
        }

        private static void NewMethod(HomePageObjectModel homePageModel)
        {
            homePageModel.LearnAboutUsClick();
        }

        [Fact]
        public void LearnAboutClick_Cancel()
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            homePageModel.LearnAboutUsClick();

            WebDriverWait wait = new WebDriverWait(chromeDriverFixture.Driver, TimeSpan.FromSeconds(5));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());              

            Assert.Equal("Do you want to learn more about us?", alert.Text);             

            alert.Dismiss();

            homePageModel.EnsurePageIsLoaded();
        }

        [Fact]
        public void NotDisplayCookieUseMessage() 
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            chromeDriverFixture.Driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
            chromeDriverFixture.Driver.Navigate().Refresh();

            Assert.False(homePageModel.IsCookieMessagePresent);

            chromeDriverFixture.Driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
            chromeDriverFixture.Driver.Navigate().Refresh();

            Assert.True(homePageModel.IsCookieMessagePresent);
        }

        [Fact]
        [UseReporter(typeof(BeyondCompare4Reporter))]
        public void RenderAboutPage()
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            ITakesScreenshot screenshotAboutPageDriver = (ITakesScreenshot)chromeDriverFixture.Driver;

            Screenshot screenshotAboutPage = screenshotAboutPageDriver.GetScreenshot();
            screenshotAboutPage.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);

            FileInfo file = new FileInfo("aboutpage.bmp");

            Approvals.Verify(file);
        }

        [Fact]
        public void OpenContactFooterLinkNewTab()
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            homePageModel.ClickContactFooterLink();
            ReadOnlyCollection<string> tabs =  homePageModel.GetAllTabs();

            string homePageTab = tabs[0];
            string contactPageTab = tabs[1];

            chromeDriverFixture.Driver.SwitchTo().Window(contactPageTab);

            Assert.EndsWith("/Home/Contact", chromeDriverFixture.Driver.Url);
        }
    }
}
