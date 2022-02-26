using System;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Product
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }

        public string? ShortDesc { get; set; }

        public string? LongDesc { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Status { get; set; }

        public bool IsPublished { get; set; } = true;

        public Guid CategoryId { get; set; }
    }
}
