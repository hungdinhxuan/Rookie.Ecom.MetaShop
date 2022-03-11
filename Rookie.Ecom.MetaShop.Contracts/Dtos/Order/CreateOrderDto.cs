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

        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Line 1 is required")]
        [MaxLength(255)]
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [MaxLength(255)]

        public string Province { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(255)]
        public string Country { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public string Note { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

    }
}
