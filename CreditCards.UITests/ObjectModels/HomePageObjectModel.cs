using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CreditCards.UITests.ObjectModels
{
    public class HomePageObjectModel : BasePageObjectModel
    {
        public const string AboutPageUrl = "http://localhost:44108/Home/About";

        public override string PageUrl => "http://localhost:44108/";      
        public override string PageTitle => "Home Page - Credit Cards";

        public HomePageObjectModel(IWebDriver driver) : base(driver) { }

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

        public ApplicationPageObjectModel CarouselEasyApplyNow()
        {
            string script = @"document.evaluate('//a[text()[contains(.,\'Easy: Apply Now!\')]]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);

            return new ApplicationPageObjectModel(driver);
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
            string script = @" document.getElementsByClassName('btn btn-default customer-service-apply-now')[0].click();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);

            return new ApplicationPageObjectModel(driver);
        }

        public ApplicationPageObjectModel ClickRandomGreetingFindingByPartialText()
        {
            driver.FindElement(By.PartialLinkText("- Apply Now!")).Click();
            return new ApplicationPageObjectModel(driver);
        }

        public ApplicationPageObjectModel ClickRandomGreetingFindingByAbsolueXPath()
        {
            driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a")).Click();
            return new ApplicationPageObjectModel(driver);
        }

        internal ApplicationPageObjectModel ClickRandomGreetingFindingByRelativeXPath()
        {            
            driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]")).Click();
            return new ApplicationPageObjectModel(driver);
        }
    }
}