
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rookie.Ecom.MetaShop.DataAccessor.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            UpdatedDate = DateTime.Now;
            CreatedDate = UpdatedDate;
        }
        [Key]
        public Guid Id { get; set; }
        [Column("updated_date")]
        [DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }

        [Column("created_date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }


        [Column("created_by")]
        public Guid? CreatedBy { get; set; }

        [Column("updated_by")]
        public Guid? UpdatedBy { get; set; }

        [Column("deleted_by")]
        public Guid? DeletedBy { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
