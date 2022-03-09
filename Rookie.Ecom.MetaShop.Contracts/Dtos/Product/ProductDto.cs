using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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


        public bool IsFeatured { get; set; }

        public bool IsPublished { get; set; } = true;

        public Guid CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        public List<ProductPictureDto> ProductPictures { get; set; }
    }
}
