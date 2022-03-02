using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class ProductPicture : BaseEntity
    {
        [StringLength(maximumLength: 250)]
        public string PictureUrl { get; set; }

        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
