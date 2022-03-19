using System;

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
