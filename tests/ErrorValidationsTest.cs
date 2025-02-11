using NUnitFrameworkDesign.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NUnitFrameworkDesign.tests
{
    /// <summary>
    /// Clase que contiene pruebas de validación de errores.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ErrorValidationsTest : BaseTest
    {
        /// <summary>
        /// Prueba que verifica la validación de error al intentar iniciar sesión con credenciales inválidas.
        /// </summary>
        [Test]
        [Category("ErrorValidation")]
        public void loginErrorValidation()
        {
            // Instancia la página de inicio
            LandingPage landingPage = new LandingPage(Driver);
            landingPage.goTo();

            // Intenta iniciar sesión con credenciales incorrectas
            landingPage.loginApplication("dagscr@gmail.com", "Derbys05");

            // Verifica que el mensaje de error sea el esperado
            Assert.That(landingPage.getErrorMessage(), Is.EqualTo("Invalid email or password"));
        }

        /// <summary>
        /// Prueba que verifica si un producto específico está presente en el catálogo.
        /// </summary>
        [Test]
        [Category("ErrorValidation")]
        public void verifyProductName()
        {
            // Instancia la página de inicio
            LandingPage landingPage = new LandingPage(Driver);
            landingPage.goTo();

            // Inicia sesión con credenciales válidas
            ProductCatalogue productCatalogue = landingPage.loginApplication("dagoscr@gmail.com", "Derbys05");

            // Verifica si el producto especificado está presente en el catálogo
            bool match = productCatalogue.verifyProductName("ADIDAS ORIGINA");

            // Valida que el producto se haya encontrado
            Assert.That(match, Is.True, "Product not found in catalogue");
        }
    }
}
