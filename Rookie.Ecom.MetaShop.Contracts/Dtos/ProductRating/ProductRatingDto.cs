using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating
{
    public class ProductRatingDto : BaseDto
    {

        public string Comment { get; set; }

        public float Rating { get; set; }

        public Guid ProductId { get; set; }

        public bool IsRated { get; set; }

        public Guid OrderItemId { get; set; }
        public OrderItemDto OrderItem { get; set; }

    }
}
