using WeekFive.Models;

public interface IOrderServicee
{
    int CreateOrder(OrderCreateDto dto);
    int GetTotalProductsPurchased();
    decimal GetTotalRevenue();
    Order GetOrderDetails(int orderId);
}