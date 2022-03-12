using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto);
        Task<List<OrderDto>> GetListOrderByUserIdAsync(Guid userId);
    }
}
