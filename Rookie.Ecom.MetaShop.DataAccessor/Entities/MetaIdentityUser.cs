using Microsoft.AspNetCore.Identity;
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
    }
}
