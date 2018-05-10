namespace SandroMancusoTraining_Project4
{
    public interface IPaymentGateway
    {
        void ProcessPayment(PaymentDetails paymentDetails);
    }
}