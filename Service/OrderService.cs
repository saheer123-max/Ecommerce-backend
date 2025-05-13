using WeekFive.Models;

public interface IOrderService
{
    int CreateOrder(OrderCreateDto dto);
}

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public int CreateOrder(OrderCreateDto dto)
    {
        var order = new Order
        {
            UserId = dto.UserId,
            Status = "Authorized",
            OrderDate = DateTime.Now,
            OrderDetails = new List<OrderDetail>()
        };

        foreach (var item in dto.Items)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product != null)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }
        }

        _context.Orders.Add(order);
        _context.SaveChanges();

        return order.Id;
    }
}
