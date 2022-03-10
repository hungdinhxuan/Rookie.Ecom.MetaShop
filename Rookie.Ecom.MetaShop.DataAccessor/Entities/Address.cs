using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using System;

namespace Rookie.Ecom.MetaShop.Identity.Entities
{
    public class Address : BaseEntity
    {
        public Guid Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}
