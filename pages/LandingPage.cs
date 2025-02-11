
using OpenQA.Selenium;

namespace NUnitFrameworkDesign.pages
{
    public class LandingPage : BasePage
    {
        IWebDriver driver;

        /**
        * Constructor de la clase LandingPage.
        * 
        * @param driver WebDriver para interactuar con el navegador.
         */
        public LandingPage(IWebDriver driver) : base(driver)
        {

            this.driver = driver;
        }

        // Localizadores de elementos
        private IWebElement userEmail => driver.FindElement(By.Id("userEmail")); // Campo de entrada para el correo electrónico

        private IWebElement userPassword => driver.FindElement(By.Id("userPassword")); // Campo de entrada para la contraseña

        private IWebElement btnLogin => driver.FindElement(By.Id("login"));  // Botón para iniciar sesión

        private IWebElement errorMessage => driver.FindElement(By.CssSelector("[class*='flyInOut']")); // Mensaje de error en caso de credenciales inválidas


        /// <summary>
        /// Realiza el inicio de sesión en la aplicación con el correo y la contraseña proporcionados.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Instancia de ProductCatalogue si el login es exitoso.</returns>
        public ProductCatalogue loginApplication(String email, String password)
        {

            userEmail.SendKeys(email);
            userPassword.SendKeys(password);
            btnLogin.Click();

            ProductCatalogue productCatalogue = new ProductCatalogue(driver);
            return productCatalogue;

        }

        /// <summary>
        /// Obtiene el mensaje de error que aparece al fallar el inicio de sesión.
        /// </summary>
        /// <returns>Texto del mensaje de error.</returns>
        public String getErrorMessage()
        {
            waitForWebElementToAppear(errorMessage);
            return errorMessage.Text;
        }

        /// <summary>
        /// Navega a la URL de la aplicación web.
        /// </summary>
        public void goTo()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/client");
        }

    }
}
