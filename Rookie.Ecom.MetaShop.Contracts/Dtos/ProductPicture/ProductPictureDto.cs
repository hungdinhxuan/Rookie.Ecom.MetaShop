using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Text.Json.Serialization;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture
{
    public class ProductPictureDto : BaseDto
    {
        public string PictureUrl { get; set; }

        public Guid ProductId { get; set; }

        /*[JsonIgnore]
        public ProductDto Product { get; set; }*/
    }
}
