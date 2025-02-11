using NUnitFrameworkDesign.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NUnitFrameworkDesign.tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ErrorValidationsTest : BaseTest
    {
        [Test]
        [Category("ErrorValidation")]
        public void loginErrorValidation() { 
            LandingPage landingPage = new LandingPage(Driver);
            landingPage.goTo();

            landingPage.loginApplication("dagscr@gmail.com", "Derbys05");

            Assert.That(landingPage.getErrorMessage(), Is.EqualTo("Invalid email or password"));
        }

        [Test]
        [Category("ErrorValidation")]
        public void verifyProductName() {

            LandingPage landingPage = new LandingPage(Driver);
            landingPage.goTo();

            ProductCatalogue productCatalogue = landingPage.loginApplication("dagoscr@gmail.com", "Derbys05");

            bool match = productCatalogue.verifyProductName("ADIDAS ORIGINAL");

            Assert.That(match, Is.True, "Product not found in catalogue");
        }
    }
}
