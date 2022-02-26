using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
