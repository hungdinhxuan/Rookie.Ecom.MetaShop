using System;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }


        public DateTime CreatedDate { get; set; }


        public DateTime UpdatedDate { get; set; }


        public Guid? CreatedBy { get; set; }


        public Guid? UpdatedBy { get; set; }


        public Guid? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
