using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System.Collections.Generic;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public string ImageUrl { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
