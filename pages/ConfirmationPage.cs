using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;

namespace NUnitFrameworkDesign.pages
{
    public class ConfirmationPage
    {
        private IWebDriver driver;

        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Localizador del mensaje de confirmación de la compra
        private IWebElement confirmMessage => driver.FindElement(By.CssSelector(".hero-primary")); // Mensaje de confirmación de la compra

    
        public string getConfirmMessageText()
        {

            return confirmMessage.Text;
        }
    }
}
