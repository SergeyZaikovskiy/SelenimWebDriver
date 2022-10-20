using OpenQA.Selenium;
using System;

namespace CreditCards.UITests.ObjectModels
{
    public class BasePageObjectModel
    {
        public readonly IWebDriver driver;

        public virtual string PageUrl { get; }

        public virtual string PageTitle { get;}

        public BasePageObjectModel(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(PageUrl);
            EnsurePageIsLoaded();
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
    }
}
