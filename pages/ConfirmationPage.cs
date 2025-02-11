using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;

namespace NUnitFrameworkDesign.pages
{
    /// <summary>
    /// Representa la página de confirmación de la compra donde el usuario puede ver el mensaje de confirmación.
    /// </summary>
    public class ConfirmationPage
    {
        private IWebDriver driver;

        /// <summary>
        /// Constructor de la clase ConfirmationPage.
        /// Inicializa la clase con el WebDriver utilizado para interactuar con la página de confirmación.
        /// </summary>
        /// <param name="driver">El WebDriver utilizado para interactuar con la página.</param>
        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Selectores

        /// <summary>
        /// El mensaje de confirmación de la compra.
        /// Utiliza un selector CSS para localizar el mensaje de confirmación en la página.
        /// </summary>
        private IWebElement confirmMessage => driver.FindElement(By.CssSelector(".hero-primary"));

        /// <summary>
        /// Obtiene el texto del mensaje de confirmación mostrado en la página.
        /// </summary>
        /// <returns>El texto del mensaje de confirmación de la compra.</returns>
        public string getConfirmMessageText()
        {
            // Retorna el texto del mensaje de confirmación
            return confirmMessage.Text;
        }
    }
}

