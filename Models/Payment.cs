// Models/Payment.cs
using WeekFive.Enums;
using WeekFive.Models;
public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public Order Order { get; set; }
}

