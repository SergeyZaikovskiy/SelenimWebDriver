using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    }
}
