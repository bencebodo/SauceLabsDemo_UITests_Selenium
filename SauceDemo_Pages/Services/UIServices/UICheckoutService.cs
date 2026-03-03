using Microsoft.Extensions.Logging;
using SauceDemo_Pages.PageObjects;
using Serilog;

namespace SauceDemo_Core.Services.UIServices
{
    public class UICheckoutService
    {
        private CheckoutPage _checkoutPage;
        private OverviewPage _overviewPage;
        private ThankYouPage _thankyouPage;
        private readonly ILogger<UICheckoutService> _logger;

        public UICheckoutService(CheckoutPage checkoutPage, OverviewPage overviewPage, ThankYouPage thankYouPage, ILogger<UICheckoutService> logger)
        {
            _checkoutPage = checkoutPage;
            _overviewPage = overviewPage;
            _thankyouPage = thankYouPage;
            _logger = logger;
        }

        public void ContinueToOverview()
        {
            _logger.LogInformation("Continuing to overview page by entering checkout information and clicking continue.");
            _logger.LogDebug("Entering first name, last name, and zip code for checkout.");
            _checkoutPage.EnterFirstName("TestFirst");
            _checkoutPage.EnterLastName("TestLast");
            _checkoutPage.EnterZip("Test1111");
            _logger.LogDebug("Clicking on continue button to proceed to overview page.");
            _checkoutPage.ClickOnContinue();
        }

        public double GetSubTotal()
        {
            _logger.LogDebug("Getting subtotal price from overview page.");
            return _overviewPage.ParsePrice(_overviewPage.GetSubTotal());
        }

        public void FinishOrder()
        {
            _logger.LogInformation("Finishing order by clicking on finish button on overview page.");
            _overviewPage.ClickOnFinish_Button();
        }

        public bool IsOrderCompleted()
        {
            _logger.LogDebug("Checking if order is completed by verifying the presence of successful order image on thank you page.");
            _logger.LogInformation("Order completion status: {IsCompleted}", _thankyouPage.IsSuccesfulOrderImgElementPresent());
            return _thankyouPage.IsSuccesfulOrderImgElementPresent();
        }
    }
}
