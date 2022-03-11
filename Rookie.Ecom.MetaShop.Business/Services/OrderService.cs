using AutoMapper;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _baseRepository;
        private readonly IMapper _mapper;

        public OrderService(IBaseRepository<Order> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
                throw new ArgumentNullException(nameof(createOrderDto));
            Order order = _mapper.Map<Order>(createOrderDto);
            order = await _baseRepository.AddAsync(order);
            return _mapper.Map<OrderDto>(order);
        }


    }
}
