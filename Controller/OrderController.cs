using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeekFive.DTO;
using WeekFive.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    // OrderController.cs
    [HttpPost("create")]
    public IActionResult CreateOrder([FromBody] OrderCreateDto dto)
    {
        var orderId = _orderService.CreateOrder(dto); // Now returns int
        return Ok(new { OrderId = orderId, Message = $"Order #{orderId} created!" });
    }
}
