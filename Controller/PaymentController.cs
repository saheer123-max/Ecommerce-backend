using Microsoft.AspNetCore.Mvc;
using WeekFive.DTO;
using WeekFive.Services;
using WeekFive.Models;
using Microsoft.AspNetCore.Authorization;

namespace WeekFive.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public IActionResult MakePayment([FromBody] PaymentDto paymentDto)
        {
            var payment = _paymentService.CreatePayment(paymentDto);
            return Ok(payment);
        }
    }
}
