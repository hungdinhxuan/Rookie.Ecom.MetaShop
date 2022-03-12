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

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        [Required]
        [MaxLength(255)]
        public string Province { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }

        public string Note { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
