using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NUnitFrameworkDesign.pages
{
    /// <summary>
    /// Clase base que contiene métodos comunes para todas las páginas del framework.
    /// Proporciona métodos de espera y navegación dentro de la aplicación web.
    /// </summary>
    public class BasePage
    {
        /// <summary>
        /// Instancia de IWebDriver utilizada para interactuar con la página web.
        /// </summary>
        protected IWebDriver driver;

        /// <summary>
        /// Constructor de la clase BasePage.
        /// </summary>
        /// <param name="driver">Instancia de IWebDriver utilizada para interactuar con la página.</param>
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// Botón que redirige al carrito de compras.
        /// </summary>
        private IWebElement btnCart => driver.FindElement(By.CssSelector("[routerlink*='cart']"));

        /// <summary>
        /// Botón que redirige a la lista de órdenes del usuario.
        /// </summary>
        private IWebElement btnOrders => driver.FindElement(By.CssSelector("[routerlink*='orders']"));

        /// <summary>
        /// Espera hasta que un elemento sea visible en la página.
        /// </summary>
        /// <param name="findBy">Ubicación del elemento a esperar.</param>
        public void waitForElementToAppear(By findBy)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(findBy).Displayed);
        }

        /// <summary>
        /// Espera hasta que un elemento específico sea visible en la página.
        /// </summary>
        /// <param name="element">Elemento web a esperar.</param>
        public void waitForWebElementToAppear(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => element.Displayed);
        }

        /// <summary>
        /// Espera un tiempo determinado para que un elemento desaparezca.
        /// (Actualmente implementado con un simple Thread.Sleep, se recomienda mejorar esta implementación).
        /// </summary>
        public void waitForElementToDisappear()
        {
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Navega a la página del carrito de compras.
        /// </summary>
        /// <returns>Instancia de la página CartPage.</returns>
        public CartPage goToCartPage()
        {
            btnCart.Click();
            return new CartPage(driver);
        }
    }
}
