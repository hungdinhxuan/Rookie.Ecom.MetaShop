﻿
using Rookie.Ecom.MetaShop.Identity.Data;
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
            CreatedDate = DateTime.Now;
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
        public string? CreatedBy { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }


        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
