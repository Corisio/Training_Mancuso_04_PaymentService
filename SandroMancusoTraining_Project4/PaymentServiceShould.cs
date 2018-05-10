using System;
using Microsoft.Win32;
using Moq;
using NUnit.Framework;

namespace SandroMancusoTraining_Project4
{
    [TestFixture]
    public class PaymentServiceShould
    {
        private Mock<IUserValidationService> _userValidationService;
        private Mock<IPaymentGateway> _paymentGateway;
        private PaymentService _paymentService;
        private User _user;
        private PaymentDetails _paymentDetails;

        [SetUp]
        public void SetUp()
        {
            _userValidationService = new Mock<IUserValidationService>();
            _paymentGateway = new Mock<IPaymentGateway>();

            _paymentService = new PaymentService(_userValidationService.Object, _paymentGateway.Object);
            _user = new User();
            _paymentDetails = new PaymentDetails();
        }

        [Test]
        public void throw_an_exception_when_the_user_is_not_valid()
        {
            GivenUserValidationResponse(false);

            Assert.Throws<UserInvalidException>(() => _paymentService.ProcessPayment(_user, _paymentDetails));
        }

        [Test]
        public void call_the_payment_gateway_when_the_user_is_valid()
        {
            GivenUserValidationResponse(true);

            WhenProcessingPayment();

            ThenPaymentGatewayShouldBeInvoked(Times.Once());
        }

        [Test]
        public void not_call_the_payment_gateway_when_the_user_is_not_valid()
        {
            GivenUserValidationResponse(false);

            WhenProcessingPayment();

            ThenPaymentGatewayShouldBeInvoked(Times.Never());
        }

        private void ThenPaymentGatewayShouldBeInvoked(Times numberOfTimes)
        {
            _paymentGateway.Verify(mock => mock.ProcessPayment(It.IsAny<PaymentDetails>()), numberOfTimes);
        }

        private void GivenUserValidationResponse(bool isValid)
        {
            _userValidationService.Setup(c => c.ValidateUser(_user)).Returns(isValid);
        }

        private void WhenProcessingPayment()
        {
            try
            {
                _paymentService.ProcessPayment(_user, _paymentDetails);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
