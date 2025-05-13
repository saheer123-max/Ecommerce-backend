using System.ComponentModel.DataAnnotations;

namespace WeekFive.DTO
{
    public class AddToWishlistDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}