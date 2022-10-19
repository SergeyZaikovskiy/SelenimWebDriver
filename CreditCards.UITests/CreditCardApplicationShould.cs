namespace CreditCards.UITests
{
    using CreditCards.UITests.ObjectModels;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;   
    using Xunit;
    using Xunit.Abstractions;   

    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould : IClassFixture<ChromeDriverFixture>
    {
        private readonly ChromeDriverFixture chromeDriverFixture;

        // private readonly ITestOutputHelper testOutput;
        //public CreditCardApplicationShould(ITestOutputHelper testOutput)
        //{
        //    this.testOutput = testOutput;
        //}

        public CreditCardApplicationShould(ChromeDriverFixture chromeDriverFixture)
        {
            this.chromeDriverFixture = chromeDriverFixture;
        }

        [Fact]
        public void BeInitiateFromHomePage_NewLowRate() 
        {           
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            ApplicationPageObjectModel applicationPage =  homePageModel.ApplyLowRateClick();

            applicationPage.EnsurePageIsLoaded();           
        }        

        [Fact]
        public void BeInitiateFromHomePage_EasyApplication()
        {           
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            homePageModel.WaitForCarouselEasyApplyNow();
            ApplicationPageObjectModel applicationPageObject = homePageModel.EasyApplyNowLinkClick();

            applicationPageObject.EnsurePageIsLoaded();
        }

        [Fact]
        public void BeInitiateFromHomePage_CustomerService()
        {
            var homePageModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageModel.NavigateTo();

            // homePageModel.SlideNextClick(3);
            homePageModel.WaitForCarouselCustomerService();
            ApplicationPageObjectModel applicationPageObject = homePageModel.CustomerServiceApplyNowLinkClick();

            applicationPageObject.EnsurePageIsLoaded();            
        }

        // [Fact]
        // public void BeInitiateFromHomePage_CustomerService_ImplicitWait_Fail()
        // {           
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Settings implicit wait");
        //     driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);           
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to {HomePageUrl}");
        //     driver.Navigate().GoToUrl(HomePageUrl);           
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
        //     var applyLowRateButton = driver.FindElement(By.ClassName("customer-service-apply-now"));           
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed {applyLowRateButton.Displayed} and enable {applyLowRateButton.Enabled}");
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Click");
        //     if (applyLowRateButton.Displayed)
        //     {
        //         applyLowRateButton.Click();
        //     }
        //     DemoHelper.Pause();           
        //     Assert.NotEqual("Credit Card Application - Credit Cards", driver.Title);
        //     Assert.NotEqual(ApplyPageUrl, driver.Url);
        //     DemoHelper.Pause();
        // }
           
        // [Fact]
        // public void BeInitiateFromHomePage_CustomerService_ImplicitWait()
        // {            
        //     //testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to {HomePageUrl}");
        //     // driver.Navigate().GoToUrl(HomePageUrl);           
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
        //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));                 
        //     Func<IWebDriver, IWebElement> findEnableAndVisible = delegate (IWebDriver d)
        //     {
        //         var element = d.FindElement(By.ClassName("customer-service-apply-now"));           
        //         if(element is null)
        //         {
        //             throw new NotFoundException();
        //         }           
        //         if(element.Enabled && element.Displayed)
        //         {
        //             return element;
        //         }           
        //         throw new NotFoundException();           
        //     };           
        //     IWebElement appleLink = wait.Until(findEnableAndVisible);           
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Found elemnt displayed {appleLink.Displayed} and enable {appleLink.Enabled}");
        //     testOutput.WriteLine($"{DateTime.Now.ToLongTimeString()} Click");
        //     appleLink.Click();
        //     DemoHelper.Pause();           
        //     Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
        //     //Assert.Equal(ApplyPageUrl, driver.Url);
        // }

        [Fact]
        public void BeInitiateFromHomePage_RandomGreeting()
        {
            HomePageObjectModel homePageObjectModel = new HomePageObjectModel(chromeDriverFixture.Driver);
            homePageObjectModel.NavigateTo();

            homePageObjectModel.ClickRandomGreeting();

            Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            //Assert.Equal(ApplyPageUrl, driver.Url);
        }

        [Fact]
        public void BeInitiateFromHomePage_RandomGreeting_AbsoluteXPath()
        {
            //driver.Navigate().GoToUrl(HomePageUrl);
            DemoHelper.Pause();

            var buttonNext = driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));
            buttonNext.Click();
            DemoHelper.Pause();

            Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            //Assert.Equal(ApplyPageUrl, driver.Url);
        }

        [Fact]
        public void BeInitiateFromHomePage_RandomGreeting_RelativeXPath()
        {
            //driver.Navigate().GoToUrl(HomePageUrl);
            DemoHelper.Pause();

            // Xpather.com
            var buttonNext = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
            buttonNext.Click();
            DemoHelper.Pause();

            Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            //Assert.Equal(ApplyPageUrl, driver.Url);
        }

        [Fact]
        public void BeSubmitted_Valid_Form()
        {
            const string firstName = "Sergey";
            const string lastName = "Super";
            const int FrequentFlyerNumber = 10;
            const int Age = 18;
            const decimal GrossAnnualIncome = 10000M;

            ApplicationPageObjectModel applicationPageObject = new ApplicationPageObjectModel(chromeDriverFixture.Driver);
            applicationPageObject.NavigateTo();

            applicationPageObject.InputFirstName(firstName);
            applicationPageObject.InputLastName(lastName);
            applicationPageObject.InputFrequentFlyerNumber(FrequentFlyerNumber.ToString());
            applicationPageObject.InputAge(Age.ToString());
            applicationPageObject.InputGrossAnnualIncome(GrossAnnualIncome.ToString());
            applicationPageObject.ChooseMartialStatusSingle();
            applicationPageObject.ChooseBusinessSourceAsTV();
            applicationPageObject.AcceptTerms();

            ApplicationCompletePageModel applicationCompletePageModel =  applicationPageObject.SubmitApplication();

            applicationCompletePageModel.EnsurePageIsLoaded();

            Assert.StartsWith("Application Complete", driver.Title);
            Assert.NotEmpty(applicationCompletePageModel.ReferenceNumber);
            Assert.Equal("ReferredToHuman", applicationCompletePageModel.Decision);
            Assert.Equal(firstName + " " + lastName, applicationCompletePageModel.FullName);
            Assert.Equal(Age.ToString(), applicationCompletePageModel.Age);              
            Assert.Equal(GrossAnnualIncome.ToString(), applicationCompletePageModel.GrossIncome);
            Assert.Equal("Single", applicationCompletePageModel.RelationshipStatus);
            Assert.Equal("TV", applicationCompletePageModel.BusinessSource);
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

            ApplicationPageObjectModel applicationPageObject = new ApplicationPageObjectModel(chromeDriverFixture.Driver);
            applicationPageObject.NavigateTo();

            applicationPageObject.InputFirstName(firstName);              
            applicationPageObject.InputFrequentFlyerNumber(FrequentFlyerNumber.ToString());
            applicationPageObject.InputAge(invalidAge.ToString());
            applicationPageObject.InputGrossAnnualIncome(GrossAnnualIncome.ToString());
            applicationPageObject.ChooseMartialStatusSingle();
            applicationPageObject.ChooseBusinessSourceAsTV();
            applicationPageObject.AcceptTerms();
            applicationPageObject.SubmitApplication();

            var validationErrors = applicationPageObject.ValidatioinErrorMessages;
                
            Assert.Equal(2, applicationPageObject.ValidatioinErrorMessages.Count);
            Assert.Contains("Please provide a last name", applicationPageObject.ValidatioinErrorMessages);
            Assert.Contains("You must be at least 18 years old", applicationPageObject.ValidatioinErrorMessages);

            //Fix validation's errors
            applicationPageObject.InputLastName(lastName);
            applicationPageObject.ClearAge();
            applicationPageObject.InputAge(validAge.ToString());

            ApplicationCompletePageModel applicationCompletePageModel =  applicationPageObject.SubmitApplication();

            applicationCompletePageModel.EnsurePageIsLoaded();
        }
    }
}
