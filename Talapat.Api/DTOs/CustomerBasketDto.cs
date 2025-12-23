using System.ComponentModel.DataAnnotations;

namespace Talapat.Api.DTOs
{
    public class CustomerBasketDto
    {
        [Required]

        public string Id { get; set; } = null!;
        [Required]

        public List<BasketItemDto> Items { get; set; } = null!;
    }
}
