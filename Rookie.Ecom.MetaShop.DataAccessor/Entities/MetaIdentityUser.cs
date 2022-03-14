using Microsoft.AspNetCore.Identity;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rookie.Ecom.MetaShop.Identity.Data
{
    public class MetaIdentityUser : IdentityUser
    {
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

        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }
        public List<ProductRating> ProductRatings { get; set; }

    }
}
