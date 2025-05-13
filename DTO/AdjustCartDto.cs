using System.ComponentModel.DataAnnotations;

namespace WeekFive.DTO
{
    public class AdjustCartDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Delta { get; set; }
    }
}