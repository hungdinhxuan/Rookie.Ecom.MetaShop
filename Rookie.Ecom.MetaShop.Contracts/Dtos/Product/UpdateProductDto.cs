using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Product
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string? ShortDesc { get; set; }

        public string? LongDesc { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Status { get; set; }

        public bool IsPublished { get; set; } = true;

        public Guid CategoryId { get; set; }

        public List<CreateProductPictureDto> ProductPictureDtos { get; set; }
    }
}
