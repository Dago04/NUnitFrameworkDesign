using OpenQA.Selenium;

namespace NUnitFrameworkDesign.pages
{
    public class CheckOutPage : BasePage
    {
        private IWebDriver driver;

        public CheckOutPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        private IWebElement btnPlacerOrder => driver.FindElement(By.CssSelector(".action__submit")); // Botón para confirmar la orden
        private IWebElement Country => driver.FindElement(By.CssSelector("[placeholder*='Country']")); // Campo de entrada para seleccionar el país
        private IList<IWebElement> suggestionList => driver.FindElements(By.CssSelector(".ta-results span")); // Lista de sugerencias desplegadas al ingresar el país

        private By suggestions = By.CssSelector(".ta-results"); // Selector para la lista de sugerencias
        private By toastMessage = By.CssSelector("#toast-container"); // Selector del mensaje emergente tras enviar la orden

 
        public void selectCountry(String countryName)
        {
            Country.SendKeys(countryName);
            waitForElementToAppear(suggestions); // Espera a que aparezca la lista de sugerencias

            foreach (WebElement suggestion in suggestionList)
            {
                if (suggestion.Text.Equals(countryName))
                {
                    suggestion.Click();
                    break;
                }
            }
        }

        public ConfirmationPage submitOrder()
        {

            btnPlacerOrder.Click();
            waitForElementToAppear(toastMessage); //Espera a que aparezca el mensaje de confirmación

            ConfirmationPage orderPage = new ConfirmationPage(driver);
            return orderPage;
        }

    }
}
