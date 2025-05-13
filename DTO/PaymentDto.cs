public class PaymentDto
{
    public decimal Amount { get; set; }
    public int OrderId { get; set; } // Only OrderId needed (not ProductId)
}