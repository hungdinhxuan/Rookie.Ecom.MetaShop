using System.ComponentModel.DataAnnotations;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Product
{
    public class ProductItemCartDto
    {
        public ProductDto Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity should be greater than 1")]
        public int Quantity { get; set; }
    }
}
