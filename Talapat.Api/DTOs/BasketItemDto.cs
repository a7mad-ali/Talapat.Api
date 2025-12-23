using System.ComponentModel.DataAnnotations;

namespace Talapat.Api.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; } = null!;
        [Required]

        public string PictureUrl { get; set; } = null!;
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="price must be greater that zero.")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be  one item at least.")]

        public int Quantity { get; set; }
        [Required]

        public string Category { get; set; } = null!;
        [Required]

        public string Brand { get; set; } = null!;
    }
}
