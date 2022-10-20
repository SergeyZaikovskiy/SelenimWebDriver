
namespace CreditCards.UITests.ObjectModels
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationCompletePageModel : BasePageObjectModel
    {
        public override string PageUrl => "http://localhost:44108/Apply";
        public override string PageTitle => "Application Complete - Credit Cards";

        public ApplicationCompletePageModel(IWebDriver driver) :base(driver)
        {
        }

        public string ReferenceNumber => driver.FindElement(By.Id("ReferenceNumber")).Text;
        
        public string Decision => driver.FindElement(By.Id("Decision")).Text;
        
        public string FullName => driver.FindElement(By.Id("FullName")).Text;
        
        public string Age => driver.FindElement(By.Id("Age")).Text;
        
        public string Income => driver.FindElement(By.Id("Income")).Text;
        
        public string RelationshipStatus => driver.FindElement(By.Id("RelationshipStatus")).Text;

        public string ReferenceIncomeNumber => driver.FindElement(By.Id("ReferenceNumber")).Text;

        public string BusinessSource => driver.FindElement(By.Id("BusinessSource")).Text;

        public string GrossIncome => driver.FindElement(By.Id("Income")).Text;
    }
}
