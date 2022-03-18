using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
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

        public async Task<int> CountAsync()
        {
            return await _baseRepository.Entities.CountAsync();
        }

        public async Task<List<ProductRatingDto>> GetListProductRatingByProductIdAsync(Guid productId)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.ProductId == productId)
                .Include(o => o.Product)
                .Include(o => o.ProductRating);
            return _mapper.Map<List<ProductRatingDto>>(await query.Select(x => x.ProductRating).ToListAsync());
        }

        public Task<OrderItemDto> GetOrderItemByIdAsync(Guid id)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.Id == id).Include(x => x.Product);
            throw new NotImplementedException();
        }
    }
}
