using System.ComponentModel.DataAnnotations;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Desc { get; set; }

        [StringLength(maximumLength: 250)]
        public string ImageUrl { get; set; }
    }
}
