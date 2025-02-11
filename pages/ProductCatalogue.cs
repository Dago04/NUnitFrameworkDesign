using OpenQA.Selenium;

namespace NUnitFrameworkDesign.pages
{
    public class ProductCatalogue : BasePage
    {
        IWebDriver driver;
        public ProductCatalogue(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        By productsBy = By.CssSelector(".mb-3"); // Selector para los productos
        By addToCart = By.CssSelector(".card-body button:last-of-type"); // Botón para agregar al carrito
        By toastMessage = By.CssSelector("#toast-container"); // Mensaje de confirmación tras agregar al carrito

        private IWebElement spinner => driver.FindElement(By.CssSelector(".ng-animating")); // Spinner de carga
        private IList<IWebElement> products => driver.FindElements(productsBy); // Lista de productos

        /// <summary>
        /// Obtiene la lista de productos disponibles en el catálogo.
        /// </summary>
        /// <returns>Lista de IWebElement que representan los productos.</returns>
        public IList<IWebElement> getProductList()
        {
            waitForElementToAppear(productsBy);
            return products;
        }


        /// <summary>
        /// Busca un producto en el catálogo por su nombre.
        /// </summary>
        /// <param name="productName">Nombre del producto a buscar.</param>
        /// <returns>El IWebElement que representa el producto encontrado, o null si no se encuentra.</returns>
        public IWebElement getProductByName(string productName)
        {
            return getProductList()
                .FirstOrDefault(product => product.FindElement(By.TagName("b")).Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase));
        }

        public bool verifyProductName(string productName)
        {
            return getProductList()
                .Any(product => product.FindElement(By.TagName("b")).Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase));

        }


        /// <summary>
        /// Agrega un producto al carrito de compras basado en su nombre.
        /// </summary>
        /// <param name="productName">Nombre del producto a agregar.</param>
        public void addProductToCar(String productName)
        {
            IWebElement prod = getProductByName(productName).FindElement(addToCart);

            prod.Click();

            waitForElementToAppear(toastMessage); // Espera a que aparezca el mensaje de confirmación
            waitForElementToDisappear(); // Espera a que desaparezca el indicador de carga
        }

    }
}
