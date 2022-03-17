using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class ProductPicture : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 1500)]
        public string PictureUrl { get; set; }

        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
