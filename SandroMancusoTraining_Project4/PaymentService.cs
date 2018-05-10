using System;

namespace SandroMancusoTraining_Project4
{
    internal class PaymentService
    {
        private readonly IUserValidationService _userValidationService;
        private readonly IPaymentGateway _paymentGatewayObject;

        public PaymentService(IUserValidationService userValidationService, IPaymentGateway paymentGatewayObject)
        {
            _userValidationService = userValidationService;
            _paymentGatewayObject = paymentGatewayObject;
        }

        internal void ProcessPayment(User user, PaymentDetails paymentDetails)
        {
            if (!_userValidationService.ValidateUser(user))
            {
                throw new UserInvalidException();
            }
            _paymentGatewayObject.ProcessPayment(paymentDetails);
        }
    }
}