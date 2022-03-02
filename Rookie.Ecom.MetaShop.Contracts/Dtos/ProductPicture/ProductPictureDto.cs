using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture
{
    public class ProductPictureDto : BaseDto
    {
        public string PictureUrl { get; set; }


        public string Title { get; set; }


        public Guid ProductId { get; set; }

        public ProductDto Product { get; set; }
    }
}
