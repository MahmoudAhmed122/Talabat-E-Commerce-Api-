using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } // Guid (Unique)
        [Required]
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }

        public decimal ShippingCost { get; set; } //deliveryMethod


    }
}
