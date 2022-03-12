using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Order
{
    public class OrderDto : BaseDto
    {
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        [Required]
        [MaxLength(255)]
        public string Province { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public string Note { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
