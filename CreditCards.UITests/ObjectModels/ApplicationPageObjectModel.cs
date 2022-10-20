namespace CreditCards.UITests.ObjectModels
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ApplicationPageObjectModel : BasePageObjectModel
    {
        public override string PageUrl => "http://localhost:44108/Apply";
        public override string PageTitle => "Credit Card Application - Credit Cards";

        public ApplicationPageObjectModel(IWebDriver driver) : base(driver)
        {
        }

        public void InputFirstName(string firstName) => driver.FindElement(By.Id("FirstName")).SendKeys(firstName);

        public void InputLastName(string lastName) => driver.FindElement(By.Id("LastName")).SendKeys(lastName);

        public void InputFrequentFlyerNumber(string frequentFlyerNumber) => driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys(frequentFlyerNumber);

        public void InputAge(string age) => driver.FindElement(By.Id("Age")).SendKeys(age);

        public void InputGrossAnnualIncome(string grossAnnualIncome) => driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys(grossAnnualIncome);

        public void ChooseMartialStatusSingle() => driver.FindElement(By.Id("Single")).Click();

        public void ChooseBusinessSourceAsTV()
        {
            IWebElement businessSourceSelectedElement = driver.FindElement(By.Id("BusinessSource"));
            SelectElement businessSource = new SelectElement(businessSourceSelectedElement);
            businessSource.SelectByValue("TV");
        }

        public void AcceptTerms() => driver.FindElement(By.Id("TermsAccepted")).Click();

        public ApplicationCompletePageModel SubmitApplication()
        {
            driver.FindElement(By.Id("SubmitApplication")).Click();
            return new ApplicationCompletePageModel(driver);
        }

        public void ClearAge() => driver.FindElement(By.Id("Age")).Clear();      

        public ReadOnlyCollection<string> ValidatioinErrorMessages 
        {
            get
            {
                return driver
                    .FindElements(By.CssSelector(".validation-summary-errors > ul > li"))
                    .Select(x => x.Text)
                    .ToList()
                    .AsReadOnly();
            }
        }
    }
}
