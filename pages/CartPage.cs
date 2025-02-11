using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitFrameworkDesign.pages
{
    /// <summary>
    /// Representa la página del carrito de compras en el flujo de un eCommerce.
    /// Proporciona métodos para interactuar con los productos en el carrito y realizar el checkout.
    /// Hereda de la clase BasePage.
    /// </summary>
    public class CartPage : BasePage
    {
        private IWebDriver driver;

        /// <summary>
        /// Constructor de la clase CartPage.
        /// Inicializa la clase base BasePage y establece el WebDriver para la página del carrito.
        /// </summary>
        /// <param name="driver">El WebDriver utilizado para interactuar con la página.</param>
        public CartPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        // Selectores

        /// <summary>
        /// Lista de productos en el carrito de compras, representados por elementos Web.
        /// Utiliza un selector CSS para localizar los elementos de los productos en la página.
        /// </summary>
        private IList<IWebElement> cartProducts => driver.FindElements(By.CssSelector(".cartSection h3"));

        /// <summary>
        /// El botón de checkout para proceder al pago.
        /// Utiliza un selector CSS para localizar el botón de checkout en la página.
        /// </summary>
        private IWebElement btnCheckout => driver.FindElement(By.CssSelector(".totalRow button"));

        /// <summary>
        /// Verifica si un producto específico se encuentra en el carrito de compras.
        /// Compara el nombre del producto con los productos listados en el carrito.
        /// </summary>
        /// <param name="productName">El nombre del producto a buscar en el carrito.</param>
        /// <returns>True si el producto está presente en el carrito, false en caso contrario.</returns>
        public bool verifyProductDisplay(string productName)
        {
            // Compara el nombre del producto con los elementos del carrito de compras
            return cartProducts.Any(product => product.Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Navega a la página de checkout al hacer clic en el botón de checkout.
        /// </summary>
        /// <returns>Una nueva instancia de la página CheckOutPage, que representa la siguiente página del proceso de compra.</returns>
        public CheckOutPage goToCheckOut()
        {
            // Hace clic en el botón de checkout para proceder al pago
            btnCheckout.Click();

            // Devuelve una nueva instancia de la página de checkout
            CheckOutPage checkOutPage = new CheckOutPage(driver);

            return checkOutPage;
        }
    }
}
