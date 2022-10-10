using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Runtime.Remoting.Messaging;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using static System.Net.Mime.MediaTypeNames;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        public const string HomePageUrl = "http://localhost:44108/";
        public const string ApplyPageUrl = "http://localhost:44108/Apply";
        private readonly ITestOutputHelper testOutput;

        public CreditCardApplicationShould(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }

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

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                IWebElement applyLink = wait.Until((d)=> d.FindElement(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_EasyApplication_PrebuildConditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));

                IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();
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

        [Fact]
        public void BeInitiateFromHomePage_CustomerService_ImplicitWait_Fail()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Settings implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);

                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to {HomePageUrl}");
                driver.Navigate().GoToUrl(HomePageUrl);

                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                var applyLowRateButton = driver.FindElement(By.ClassName("customer-service-apply-now"));

                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Found elemnt displayed {applyLowRateButton.Displayed} and enable {applyLowRateButton.Enabled}");
                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Click");
                applyLowRateButton.Click();
                DemoHelper.Pause();

                Assert.NotEqual("Credit Card Application - Credit Cards", driver.Title);
                Assert.NotEqual(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_CustomerService_ImplicitWait()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to {HomePageUrl}");
                driver.Navigate().GoToUrl(HomePageUrl);

                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
                
                Func<IWebDriver, IWebElement> findEnableAndVisible = delegate (IWebDriver d)
                {
                    var element = d.FindElement(By.ClassName("customer-service-apply-now"));

                    if(element is null)
                    {
                        throw new NotFoundException();
                    }

                    if(element.Enabled && element.Displayed)
                    {
                        return element;
                    }

                    throw new NotFoundException();

                };

                IWebElement appleLink = wait.Until(findEnableAndVisible);

                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Found elemnt displayed {appleLink.Displayed} and enable {appleLink.Enabled}");
                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Click");
                appleLink.Click();
                DemoHelper.Pause();

                Assert.NotEqual("Credit Card Application - Credit Cards", driver.Title);
                Assert.NotEqual(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                var buttonNext = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                buttonNext.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_RandomGreeting_AbsoluteXPath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                var buttonNext = driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));
                buttonNext.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiateFromHomePage_RandomGreeting_RelativeXPath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                // Xpather.com
                var buttonNext = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                buttonNext.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
            }
        }
    }
}
