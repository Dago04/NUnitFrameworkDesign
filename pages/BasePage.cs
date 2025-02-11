using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitFrameworkDesign.pages
{
    public class BasePage
    {
        IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement btnCart => driver.FindElement(By.CssSelector("[routerlink*='cart']")); // Botón para abrir el carrito de compras
        private IWebElement btnOrders =>  driver.FindElement(By.CssSelector("[routerlink*='orders']")); // Botón para abrir la lista de órdenes

        public void waitForElementToAppear(By findBy) { 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(findBy).Displayed); //Espera que el elemento sea visible, verificando que esté realmente visible en la página.
        }
        public void waitForWebElementToAppear(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => element.Displayed); // Espera a que el elemento sea visible
        }

        public void waitForElementToDisappear()
        {
            Thread.Sleep(1000);
        }

        public CartPage goToCartPage()
        {
            btnCart.Click();
            CartPage cartPage = new CartPage(driver);
            return cartPage;
        }
    }

}
