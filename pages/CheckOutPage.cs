using OpenQA.Selenium;

namespace NUnitFrameworkDesign.pages
{
    /// <summary>
    /// Representa la página de checkout donde el usuario puede confirmar su orden y seleccionar su país.
    /// Hereda de la clase BasePage.
    /// </summary>
    public class CheckOutPage : BasePage
    {
        private IWebDriver driver;

        /// <summary>
        /// Constructor de la clase CheckOutPage.
        /// Inicializa la clase base BasePage y establece el WebDriver para la página de checkout.
        /// </summary>
        /// <param name="driver">El WebDriver utilizado para interactuar con la página.</param>
        public CheckOutPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        // Selectores

        /// <summary>
        /// El botón para confirmar la orden.
        /// Se localiza utilizando un selector CSS.
        /// </summary>
        private IWebElement btnPlacerOrder => driver.FindElement(By.CssSelector(".action__submit"));

        /// <summary>
        /// El campo de entrada para seleccionar el país en el checkout.
        /// Se localiza utilizando un selector CSS.
        /// </summary>
        private IWebElement Country => driver.FindElement(By.CssSelector("[placeholder*='Country']"));

        /// <summary>
        /// La lista de sugerencias que se muestran al ingresar el país.
        /// Se localiza utilizando un selector CSS.
        /// </summary>
        private IList<IWebElement> suggestionList => driver.FindElements(By.CssSelector(".ta-results span"));

        /// <summary>
        /// Selector que representa la lista de sugerencias.
        /// </summary>
        private By suggestions = By.CssSelector(".ta-results");

        /// <summary>
        /// Selector para el mensaje emergente tras enviar la orden.
        /// </summary>
        private By toastMessage = By.CssSelector("#toast-container");

        /// <summary>
        /// Selecciona un país de la lista de sugerencias al escribir el nombre del país en el campo correspondiente.
        /// Espera a que aparezca la lista de sugerencias y hace clic en el país indicado.
        /// </summary>
        /// <param name="countryName">El nombre del país que se desea seleccionar.</param>
        public void selectCountry(string countryName)
        {
            // Escribe el nombre del país en el campo de entrada
            Country.SendKeys(countryName);

            // Espera a que la lista de sugerencias aparezca
            waitForElementToAppear(suggestions);

            // Itera sobre las sugerencias y selecciona el país que coincide
            foreach (WebElement suggestion in suggestionList)
            {
                if (suggestion.Text.Equals(countryName))
                {
                    suggestion.Click();
                    break;
                }
            }
        }

        /// <summary>
        /// Envía la orden y navega a la página de confirmación.
        /// Hace clic en el botón para confirmar la orden y espera a que aparezca el mensaje de confirmación.
        /// </summary>
        /// <returns>Una nueva instancia de la página ConfirmationPage, que representa la página de confirmación de la orden.</returns>
        public ConfirmationPage submitOrder()
        {
            // Hace clic en el botón para confirmar la orden
            btnPlacerOrder.Click();

            // Espera a que aparezca el mensaje de confirmación
            waitForElementToAppear(toastMessage);

            // Devuelve una nueva instancia de la página de confirmación
            ConfirmationPage orderPage = new ConfirmationPage(driver);
            return orderPage;
        }

    }
}