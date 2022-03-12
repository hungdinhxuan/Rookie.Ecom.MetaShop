using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using System;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Order
{
    public class OrderItemDto : BaseDto
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public ProductDto Product { get; set; }
        public ProductRatingDto ProductRating { get; set; }
    }
}
