using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Order
{
    public class CreateOrderDto
    {
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

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

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public string Note { get; set; }
    }
}
