using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating
{
    public class UpdateProductRatingDto
    {
        public Guid Id { get; set; }

        [StringLength(3000)]
        public string Comment { get; set; }
        [Range(0, 5)]
        public float Rating { get; set; } = 5;

        public bool IsRated { get; set; } = false;
    }
}
