using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Product
{
    public class ProductItemCartDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
