using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDto> AddOrderItemAsync(CreateOrderItemDto createOrderItemDto);
        Task<List<OrderItemDto>> AddRangeOrderItemsAsync(List<CreateOrderItemDto> createOrderItemDtos);
    }
}
