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

        [Column("order_item_id")]
        public Guid OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }

        [Column("is_rated")]
        public bool IsRated { get; set; } = false;
    }
}
