using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating
{
    public class CreateProductRatingDto
    {
        [StringLength(3000)]
        public string Comment { get; set; }
        [Range(0, 5)]
        public float Rating { get; set; } = 5;

        public Guid ProductId { get; set; }

        public Guid OrderItemId { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsRated { get; set; } = false;
    }
}
