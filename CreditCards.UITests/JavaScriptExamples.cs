using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class JavaScriptExamples
    {
        [Fact]
        public void ClickOverlayedLink_CheckURL()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
                DemoHelper.Pause();

                string script = "document.getElementById('HiddenLink').click();";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript(script);
                DemoHelper.Pause();

                Assert.Equal("https://www.pluralsight.com/", driver.Url);
            }
        }

        [Fact]
        public void ClickOverlayedLink_LinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/jsoverlay.html");
                DemoHelper.Pause();

                string script = "return document.getElementById('HiddenLink').innerHTML;";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                string linkText = (string)js.ExecuteScript(script);
                DemoHelper.Pause();

                Assert.Equal("Go to Pluralsight", linkText);
            }
        }
    }
}