using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        public const string HomePageUrl = "http://localhost:44108/";
        public const string ApplyPageUrl = "http://localhost:44108/Apply";

        [Fact]
        public void BeInitiateFromHomePage_NewLowRate() 
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                var applyLowRateButton = driver.FindElement(By.Name("ApplyLowRate"));
                applyLowRateButton.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                var buttonNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                buttonNext.Click();
                DemoHelper.Pause();

                var applyLowRateButton = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLowRateButton.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                var buttonNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                buttonNext.Click();
                DemoHelper.Pause();

                buttonNext.Click();
                DemoHelper.Pause();

                var applyLowRateButton = driver.FindElement(By.ClassName("customer-service-apply-now"));
                applyLowRateButton.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }
    }
}
