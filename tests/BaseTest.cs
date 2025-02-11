using Microsoft.Extensions.Configuration;
using NUnitFrameworkDesign.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;


namespace NUnitFrameworkDesign.tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        private IConfigurationRoot config;

        [SetUp]
        public void setup()
        {
            // Obtiene la ruta del archivo `appsettings.json` en la carpeta raíz del proyecto
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string configPath = Path.Combine(projectRoot, "data", "appsettings.json");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"No se encontró el archivo de configuración en: {configPath}");
            }

            // Cargar la configuración desde `appsettings.json`
            var builder = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddJsonFile(configPath, optional: false, reloadOnChange: true);

            config = builder.Build();

            // Obtener valores del archivo de configuración
            string browserName = config["browser"];
            bool isHeadless = bool.Parse(config["Headless"]);

            // Iniciar el navegador
            driver = InitializeDriver(browserName, isHeadless);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }

        private IWebDriver InitializeDriver(string browserName, bool isHeadless)
        {
            if (browserName.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                var options = new ChromeOptions();
                if (isHeadless)
                {
                    options.AddArgument("--headless");
                }
                return new ChromeDriver(options);
            }
            else if (browserName.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                var options = new FirefoxOptions();
                if (isHeadless)
                {
                    options.AddArgument("--headless");
                }
                return new FirefoxDriver(options);
            }
            else if (browserName.Equals("edge", StringComparison.OrdinalIgnoreCase))
            {
                return new EdgeDriver();
            }
            else
            {
                throw new ArgumentException($"El navegador '{browserName}' no está soportado.");
            }
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
