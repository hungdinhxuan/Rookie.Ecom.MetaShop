﻿using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using System;
using System.Collections.Generic;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public string? ShortDesc { get; set; }

        public string? LongDesc { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Status { get; set; }

        public bool IsFeatured { get; set; } = true;

        public bool IsPublished { get; set; } = true;

        public Guid CategoryId { get; set; }

        public string? CreatedBy { get; set; }

        public List<CreateProductPictureDto> ProductPictureDtos { get; set; }
    }
}
