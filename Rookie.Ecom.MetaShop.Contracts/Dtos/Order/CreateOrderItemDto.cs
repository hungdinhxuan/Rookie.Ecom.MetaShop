using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Order
{
    public class CreateOrderItemDto
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }

    }
}
