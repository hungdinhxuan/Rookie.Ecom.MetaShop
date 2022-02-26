using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Column("short_desc")]
        public string? ShortDesc { get; set; }

        [Column("long_desc")]
        public string? LongDesc { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Status { get; set; }

        [Column("is_published")]
        public bool IsPublished { get; set; } = true;

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public List<ProductPicture> ProductPictures { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public List<ProductRating> ProductRating { get; set; }
    }
}
