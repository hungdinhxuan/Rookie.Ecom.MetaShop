using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class ProductRating : BaseEntity
    {
        [StringLength(3000)]
        public string Comment { get; set; }
        [Range(0, 5)]
        public float Rating { get; set; } = 5;

        [Column("product_id")]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [Column("order_id")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Column("is_rated")]
        public bool IsRated { get; set; } = false;
    }
}
