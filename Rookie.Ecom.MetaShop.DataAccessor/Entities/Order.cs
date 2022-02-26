using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class Order : BaseEntity
    {

        public string Status { get; set; }

        [DataType(DataType.Currency)]
        [Column("total_price", TypeName = "money")]
        public decimal TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public List<ProductRating> ProductRatings { get; set; }
    }
}
