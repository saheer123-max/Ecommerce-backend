using System.ComponentModel.DataAnnotations;

namespace WeekFive.DTO
{
    public class AddToCartDto
    {
        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; } = 1;
    }
}
