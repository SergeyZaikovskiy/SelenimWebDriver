namespace CreditCards.UITests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Xunit;

    public class JavaScriptExamples : IClassFixture<ChromeDriverFixture>
    {
        private readonly ChromeDriverFixture chromeDriverFixture;

        public JavaScriptExamples(ChromeDriverFixture chromeDriverFixture)
        {
            this.chromeDriverFixture = chromeDriverFixture;
            this.chromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            this.chromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
        }
    
        [Fact]
        public void ClickOverlayedLink_CheckURL()
        {
            chromeDriverFixture.Driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
            DemoHelper.Pause();

            string script = "document.getElementById('HiddenLink').click();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriverFixture.Driver;

            js.ExecuteScript(script);
            DemoHelper.Pause();

            Assert.Equal("https://www.pluralsight.com/", chromeDriverFixture.Driver.Url);            
        }

        [Fact]
        public void ClickOverlayedLink_LinkText()
        {
            chromeDriverFixture.Driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");                

            string script = "return document.getElementById('HiddenLink').innerHTML;";
            IJavaScriptExecutor js = (IJavaScriptExecutor)chromeDriverFixture.Driver;

            string linkText = (string)js.ExecuteScript(script);
            DemoHelper.Pause();

            Assert.Equal("Go to Pluralsight", linkText);           
        }
    }
}