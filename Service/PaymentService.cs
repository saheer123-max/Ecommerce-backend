using WeekFive.DTO;
using WeekFive.Models;
using WeekFive.Data;
using WeekFive.Enums;
namespace WeekFive.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public Payment CreatePayment(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                OrderId = paymentDto.OrderId, // Link to Order
                Amount = paymentDto.Amount,
                Status = PaymentStatus.Authorized
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return payment;
        }
    }
}
