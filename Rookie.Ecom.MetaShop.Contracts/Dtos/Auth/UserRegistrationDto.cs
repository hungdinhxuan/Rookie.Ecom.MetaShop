using System.ComponentModel.DataAnnotations;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Auth
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }

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
    }
}
