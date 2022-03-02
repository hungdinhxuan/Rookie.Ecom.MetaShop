using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture
{
    public class CreateProductPictureDto
    {
        public string PictureUrl { get; set; }

        public Guid ProductId { get; set; }
    }
}
