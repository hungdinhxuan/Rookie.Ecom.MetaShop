using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IBaseRepository<OrderItem> _baseRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IBaseRepository<OrderItem> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<OrderItemDto> AddOrderItemAsync(CreateOrderItemDto createOrderItemDto)
        {
            if (createOrderItemDto == null)
                throw new ArgumentNullException(nameof(createOrderItemDto));
            OrderItem orderItem = _mapper.Map<OrderItem>(createOrderItemDto);
            orderItem = await _baseRepository.AddAsync(orderItem);
            return _mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task<List<OrderItemDto>> AddRangeOrderItemsAsync(List<CreateOrderItemDto> createOrderItemDtos)
        {
            IEnumerable<OrderItem> orderItems = _mapper.Map<IEnumerable<OrderItem>>(createOrderItemDtos);
            orderItems = await _baseRepository.AddRangeAsync(orderItems);
            return _mapper.Map<List<OrderItemDto>>(orderItems.ToList());
        }

        public Task<OrderItemDto> GetOrderItemByIdAsync(Guid id)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.Id == id).Include(x => x.Product);
            throw new NotImplementedException();
        }
    }
}
