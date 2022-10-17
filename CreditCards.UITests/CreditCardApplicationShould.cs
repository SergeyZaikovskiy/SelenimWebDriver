using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
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
        public const string ContactUrl = "http://localhost:44108/Home/Contact";
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
                driver.Manage().Window.Minimize();
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

                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed {applyLowRateButton.Displayed} and enable {applyLowRateButton.Enabled}");
                testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Click");
                if (applyLowRateButton.Displayed)
                {
                    applyLowRateButton.Click();
                }
                DemoHelper.Pause();

                Assert.NotEqual("Credit Card Application - Credit Cards", driver.Title);
                Assert.NotEqual(ApplyPageUrl, driver.Url);
                DemoHelper.Pause();
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
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                
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

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyPageUrl, driver.Url);
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

        [Fact]
        public void BeSubmitted_Valid_Form()
        {
            const string firstName = "Sergey";
            const string lastName = "Super";
            const int FrequentFlyerNumber = 10;
            const int Age = 18;
            const decimal GrossAnnualIncome = 10000M;

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(ApplyPageUrl);
                DemoHelper.Pause();

                driver.FindElement(By.Id("FirstName")).SendKeys(firstName);
                driver.FindElement(By.Id("LastName")).SendKeys(lastName);
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys(FrequentFlyerNumber.ToString());
                driver.FindElement(By.Id("Age")).SendKeys(Age.ToString());
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys(GrossAnnualIncome.ToString());
                driver.FindElement(By.Id("Married")).Click();
                DemoHelper.Pause();

                IWebElement businessSourceSelectedElement = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectedElement);
                Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);

                foreach(var element in businessSource.Options)
                {
                    testOutput.WriteLine($"Value - {element.GetAttribute("value")} and Text - {element.Text}" );
                }
                Assert.Equal(5, businessSource.Options.Count);

                businessSource.SelectByValue("Email");
                DemoHelper.Pause();
                businessSource.SelectByText("Internet Search");
                DemoHelper.Pause();
                businessSource.SelectByIndex(4);
                DemoHelper.Pause();

                driver.FindElement(By.Id("TermsAccepted")).Click();
                DemoHelper.Pause();

                // driver.FindElement(By.Id("SubmitApplication")).Click();
                driver.FindElement(By.Id("Married")).Submit();
                DemoHelper.Pause();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("ReferredToHuman",driver.FindElement(By.Id("Decision")).Text);
                Assert.Equal(firstName + " " + lastName, driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal(Age.ToString(), driver.FindElement(By.Id("Age")).Text);              
                Assert.Equal(GrossAnnualIncome.ToString(), driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Married", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }

        [Fact]
        public void BeSubmitted_Invalid_Form()
        {
            const string firstName = "Sergey";
            const string lastName = "Super";
            const int FrequentFlyerNumber = 10;
            const int invalidAge = 17;
            const int validAge = 18;
            const decimal GrossAnnualIncome = 10000M;

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(ApplyPageUrl);
                DemoHelper.Pause();

                driver.FindElement(By.Id("FirstName")).SendKeys(firstName);
                
                // Skip LastName
                //driver.FindElement(By.Id("LastName")).SendKeys(lastName);

                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys(FrequentFlyerNumber.ToString());
                driver.FindElement(By.Id("Age")).SendKeys(invalidAge.ToString());
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys(GrossAnnualIncome.ToString());
                driver.FindElement(By.Id("Married")).Click();
                DemoHelper.Pause();

                IWebElement businessSourceSelectedElement = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectedElement);
                Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);

                foreach (var element in businessSource.Options)
                {
                    testOutput.WriteLine($"Value - {element.GetAttribute("value")} and Text - {element.Text}");
                }
                Assert.Equal(5, businessSource.Options.Count);

                businessSource.SelectByValue("Email");
                DemoHelper.Pause();
                businessSource.SelectByText("Internet Search");
                DemoHelper.Pause();
                businessSource.SelectByIndex(4);
                DemoHelper.Pause();

                driver.FindElement(By.Id("TermsAccepted")).Click();
                DemoHelper.Pause();

                driver.FindElement(By.Id("SubmitApplication")).Click();                
                DemoHelper.Pause();

                var validationErrors = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                Assert.Equal(2, validationErrors.Count);
                Assert.Equal("Please provide a last name", validationErrors[0].Text);
                Assert.Equal("You must be at least 18 years old", validationErrors[1].Text);

                //Fix validation's errors
                driver.FindElement(By.Id("LastName")).SendKeys(lastName);
                driver.FindElement(By.Id("Age")).Clear();
                driver.FindElement(By.Id("Age")).SendKeys(validAge.ToString());

                driver.FindElement(By.Id("SubmitApplication")).Click();
                DemoHelper.Pause();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.Equal(firstName + " " + lastName, driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal(validAge.ToString(), driver.FindElement(By.Id("Age")).Text);
                Assert.Equal(GrossAnnualIncome.ToString(), driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Married", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }

        [Fact]
        public void OpenContactFooterLinkNewTab() 
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();

                driver.FindElement(By.Id("ContactFooter")).Click();
                DemoHelper.Pause();

                ReadOnlyCollection<string> tabs = driver.WindowHandles;

                string homePageTab= tabs[0];
                string contactPageTab= tabs[1];

                driver.SwitchTo().Window(contactPageTab);

                Assert.StartsWith("Contact", driver.Title);
                Assert.Equal(ContactUrl, driver.Url);
            }
        }
    }
}
