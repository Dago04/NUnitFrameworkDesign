﻿using NUnitFrameworkDesign.data;
using NUnitFrameworkDesign.pages;


namespace NUnitFrameworkDesign.tests
{
    [TestFixture]
    public class SubmitOrderTest : BaseTest
    {

        [Test, TestCaseSource(typeof(purchaseOrderReader), nameof(purchaseOrderReader.GetTestData))]
        [Category("Purchase")]
        public void submitOrder(string email, string password, string productName)
        {
            try
            {
                string countryName = "Costa Rica";

                LandingPage landingPage = new LandingPage(Driver);
                landingPage.goTo();

                ProductCatalogue productCatalogue = landingPage.loginApplication(email, password);
                productCatalogue.addProductToCar(productName);

                CartPage cartPage = productCatalogue.goToCartPage();

                bool match = cartPage.verifyProductDisplay(productName);

                Assert.That(match, Is.True, "Product not found in cart");

                CheckOutPage checkOutPage = cartPage.goToCheckOut();

                checkOutPage.selectCountry(countryName);

                ConfirmationPage confirmationPage = checkOutPage.submitOrder();

                string confirmationMessage = confirmationPage.getConfirmMessageText();

                Assert.That(confirmationMessage, Is.EqualTo("THANKYOU FOR THE ORDER."), "Order confirmation message is incorrect");

                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }


}
