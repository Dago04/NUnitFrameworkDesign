using NUnitFrameworkDesign.pages;


namespace NUnitFrameworkDesign.tests
{
    public class SubmitOrderTest : BaseTest
    {

        [Test]
        public void submitOrder()
        {
            string productName = "ZARA COAT 3";

            LandingPage landingPage = new LandingPage(driver);
            landingPage.goTo();

            ProductCatalogue productCatalogue = landingPage.loginApplication("dagoscr@gmail.com", "Derbys05");
            productCatalogue.addProductToCar(productName);
            
            Thread.Sleep(2000);
        }
    }
}
