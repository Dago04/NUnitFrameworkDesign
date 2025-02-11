using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitFrameworkDesign.pages
{
    public class CartPage : BasePage
    {
        private IWebDriver driver;

        public CartPage(IWebDriver driver) :base(driver)
        {
            this.driver = driver;
        }

        //Selectores
        private IList<IWebElement> cartProducts => driver.FindElements(By.CssSelector(".cartSection h3")); // Lista de productos en el carrito
        private IWebElement btnCheckout => driver.FindElement(By.CssSelector(".totalRow button")); // Botón para ir a la página de checkout


        public bool verifyProductDisplay(string productName) { 
               return cartProducts.Any(product => product.Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase));
        }

        public CheckOutPage goToCheckOut()
        {

            btnCheckout.Click();

            CheckOutPage checkOutPage = new CheckOutPage(driver);

            return checkOutPage;
        }

    }
}
