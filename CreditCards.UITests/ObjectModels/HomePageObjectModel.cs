using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CreditCards.UITests.ObjectModels
{
    public class HomePageObjectModel
    {
        public const string PageUrl = "http://localhost:44108/";
        public const string AboutPageUrl = "http://localhost:44108/Home/About";
        public const string PageTitle = "Home Page - Credit Cards";

        private readonly IWebDriver driver;

        public HomePageObjectModel(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(PageUrl);
            EnsurePageIsLoaded();
        }        

        public ReadOnlyCollection<(string productName, string interestRate)> Products
        {
            get
            {
                var cells = driver.FindElements(By.TagName("td"));

                var products = new List<(string, string)>();

                for (int i = 0; i < cells.Count - 1; i+=2)
                {
                    string name = cells[i].Text;
                    string rate = cells[i+1].Text;

                    products.Add((name, rate));
                }

                return products.AsReadOnly();
            }
        }

        public void EnsurePageIsLoaded(bool checkUrlStartWithExpectedText = true)
        {
            bool isUrlCorrect;

            if (checkUrlStartWithExpectedText)
            {
                isUrlCorrect = driver.Url.StartsWith(PageUrl);
            }
            else
            {
                isUrlCorrect = driver.Url == PageUrl;
            }

            bool pageLoaded = isUrlCorrect && (PageTitle == driver.Title);

            if (!pageLoaded)
            {
                throw new Exception($"Page {PageUrl} isn't loaded correctly. Page source {driver.PageSource}");
            }
        }

        public void ClickContactFooterLink() => driver.FindElement(By.Id("ContactFooter")).Click();

        public ReadOnlyCollection<string> GetAllTabs() => driver.WindowHandles;

        public void LiveChatClick()
        {
            driver.FindElement(By.Id("LiveChat")).Click();
        }

        public void LearnAboutUsClick() => driver.FindElement(By.Id("LearnAboutUs")).Click();

        public string GetToken => driver.FindElement(By.Id("GenerationToken")).Text;

        public bool IsCookieMessagePresent => driver.FindElements(By.Id("CookiesBeingUsed")).Any();

        public ApplicationPageObjectModel ApplyLowRateClick()
        {
            driver.FindElement(By.Name("ApplyLowRate")).Click();
            return new ApplicationPageObjectModel(driver);
        }

        public void SlideNextClick(int count = 1)
        {
            IWebElement nextButton = driver.FindElement(By.CssSelector("[data-slide='next']"));

            for (int i = 0; i < count; i++)
            {
                nextButton.Click();
            }
        }

        public void WaitForCarouselEasyApplyNow()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));
            IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
        }

        public ApplicationPageObjectModel EasyApplyNowLinkClick()
        {
            driver.FindElement(By.LinkText("Easy: Apply Now!")).Click();
            return new ApplicationPageObjectModel(driver);
        }

        public void WaitForCarouselCustomerService()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(22));
            IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));
        }

        public ApplicationPageObjectModel CustomerServiceApplyNowLinkClick()
        {
            driver.FindElement(By.ClassName("customer-service-apply-now")).Click();
            return new ApplicationPageObjectModel(driver);
        }

        public void ClickRandomGreeting() => driver.FindElement(By.PartialLinkText("- Apply Now!")).Click();
    }
}