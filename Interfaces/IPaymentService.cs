using WeekFive.DTO;
using WeekFive.Models;

namespace WeekFive.Services
{
    public interface IPaymentService
    {
        Payment CreatePayment(PaymentDto paymentDto);
    }
}
