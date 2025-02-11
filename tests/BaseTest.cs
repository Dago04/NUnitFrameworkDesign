using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Microsoft.Extensions.Configuration;
using NUnitFrameworkDesign.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using NUnit.Framework.Interfaces;


namespace NUnitFrameworkDesign.tests
{
    public class BaseTest
    {


        //protected IWebDriver driver;
        private IConfigurationRoot config;
        private static ExtentReports extent;
        private ExtentTest test;

        // WebDriver aislado por hilo para pruebas en paralelo
        private static ThreadLocal<IWebDriver> threadDriver = new();
        public static IWebDriver Driver => threadDriver.Value;

        /// <summary>
        /// Configura el entorno de prueba antes de ejecutar cada test.
        /// </summary>
        /// <exception cref="FileNotFoundException">Lanza una excepción si el archivo de configuración no se encuentra.</exception>
        [SetUp]
        public void Setup()
        {
            // Obtiene la ruta del archivo `appsettings.json` en la carpeta raíz del proyecto
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string configPath = Path.Combine(projectRoot, "data", "appsettings.json");

            // Verifica si el archivo de configuración existe
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

            // Inicializar el WebDriver y asignarlo a `threadDriver`
            threadDriver.Value = InitializeDriver(browserName, isHeadless);

            // Configurar el tiempo de espera implícito y maximizar la ventana del navegador
            threadDriver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            threadDriver.Value.Manage().Window.Maximize();

            // Crear un nuevo test en el reporte de ExtentReports con el nombre del test actual
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        /// <summary>
        /// Finaliza la ejecución de cada prueba, captura una captura de pantalla en caso de fallo y cierra el WebDriver.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            // Verifica si la prueba falló
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                // Captura la pantalla y guarda la imagen con el nombre de la prueba
                string screenshotPath = GetScreenshot(TestContext.CurrentContext.Test.Name);

                // Agrega la captura de pantalla al reporte de ExtentReports con el mensaje de error
                test.Fail(TestContext.CurrentContext.Result.Message)
                    .AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                // Registra la prueba como exitosa en el reporte
                test.Pass(TestContext.CurrentContext.Result.Message + " Test passed");
            }

            // Cerrar el WebDriver después de cada prueba para liberar recursos
            if (threadDriver.Value != null)
            {
                threadDriver.Value.Quit();
                threadDriver.Value = null; // Limpiar el valor del ThreadLocal
            }
        }
        /// <summary>
        /// Inicializa el objeto de reportería ExtentReports antes de ejecutar cualquier prueba en la suite.
        /// </summary>
        [OneTimeSetUp]
        public void InitializeReport()
        {
            // Obtiene y asigna el objeto ExtentReports para generar reportes de pruebas
            extent = GetReporterObject();
        }
        /// <summary>
        /// Finaliza y guarda el reporte de pruebas generado por ExtentReports.
        /// Se ejecuta una única vez después de que todas las pruebas han finalizado.
        /// </summary>
        [OneTimeTearDown]
        public void FinalizeReport()
        {
            // Guarda y cierra el reporte generado por ExtentReports
            extent.Flush();
        }
        /// <summary>
        /// Inicializa el WebDriver según el navegador especificado y su modo de ejecución (headless o no).
        /// </summary>
        /// <param name="browserName">Nombre del navegador (chrome, firefox, edge).</param>
        /// <param name="isHeadless">Indica si el navegador debe ejecutarse en modo headless.</param>
        /// <returns>Una instancia de IWebDriver configurada para el navegador especificado.</returns>
        /// <exception cref="ArgumentException">Lanza una excepción si el navegador no está soportado.</exception>
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
        /// <summary>
        /// Obtiene o crea una instancia de ExtentReports para la generación de reportes de pruebas automatizadas.
        /// </summary>
        /// <returns>Una instancia de <see cref="ExtentReports"/> configurada.</returns>
        public static ExtentReports GetReporterObject()
        {
            if (extent == null)
            {
                // Definir la ruta donde se generará el reporte
                string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string reportDirectory = Path.Combine(projectRoot, "reports");

                // Verifica si la carpeta `reports` existe, si no, la crea
                if (!Directory.Exists(reportDirectory))
                {
                    Directory.CreateDirectory(reportDirectory);
                }

                string reportPath = Path.Combine(reportDirectory, "index.html");

                // Crear un objeto ExtentSparkReporter con la ruta del reporte
                var reporter = new ExtentSparkReporter(reportPath);

                // Configurar el nombre del reporte y el título del documento
                reporter.Config.ReportName = "Web Automation Results";
                reporter.Config.DocumentTitle = "Test Results";

                // Crear un objeto ExtentReports y adjuntar el reporter configurado
                extent = new ExtentReports();
                extent.AttachReporter(reporter);

                // Agregar información adicional sobre el sistema o el tester
                extent.AddSystemInfo("Tester", "Dago");
            }

            return extent;
        }

        public string GetScreenshot(string testCaseName)
        {
            // Limpiar el nombre del archivo de caracteres no válidos
            testCaseName = SanitizeFileName(testCaseName);

            // Agregar timestamp y ThreadId para asegurar que el nombre sea único
            testCaseName = $"{testCaseName}_{DateTime.Now:yyyyMMdd_HHmmss}_{Thread.CurrentThread.ManagedThreadId}";

            // Tomar la captura de pantalla
            ITakesScreenshot screenshotDriver = threadDriver.Value as ITakesScreenshot;
            if (screenshotDriver == null)
            {
                throw new InvalidOperationException("Driver no soporta capturas de pantalla.");
            }

            Screenshot screenshot = screenshotDriver.GetScreenshot();

            // Obtener la ruta de la carpeta raíz del proyecto
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string reportsDirectory = Path.Combine(projectRoot, "reports");

            // Verificar si el directorio existe, y si no, crear la carpeta
            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }

            // Crear la ruta completa para el archivo de imagen en la carpeta de reportes
            string screenshotPath = Path.Combine(reportsDirectory, $"{testCaseName}.png");

            // Guardar la imagen
            screenshot.SaveAsFile(screenshotPath);

            // Retornar la ruta de la captura de pantalla
            return screenshotPath;
        }

        public string SanitizeFileName(string fileName)
        {
            // Reemplaza los caracteres no válidos en Windows para nombres de archivos
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName;
        }
    }

    
}
