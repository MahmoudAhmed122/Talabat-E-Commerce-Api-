using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }
        [Required]
        [Range(1 , int.MaxValue , ErrorMessage ="Basket must have at least one item !")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be more than 0.1 pound !")]
        public decimal Price { get; set; }
    }
}