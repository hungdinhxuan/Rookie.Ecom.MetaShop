using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using Rookie.Ecom.MetaShop.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Services
{
    public class ProductRatingService : IProductRatingService
    {
        private readonly IBaseRepository<ProductRating> _baseRepository;
        private readonly IMapper _mapper;
        private readonly AspNetIdentityDbContext _aspNetIdentityDbContext;

        public ProductRatingService(IBaseRepository<ProductRating> baseRepository, IMapper mapper, AspNetIdentityDbContext aspNetIdentityDbContext)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _aspNetIdentityDbContext = aspNetIdentityDbContext;
        }

        public async Task<List<ProductRatingDto>> AddRangeProductRatingAsync(List<CreateProductRatingDto> createProductRatingDtos)
        {
            IEnumerable<ProductRating> productRatings = _mapper.Map<IEnumerable<ProductRating>>(createProductRatingDtos);
            productRatings = await _baseRepository.AddRangeAsync(productRatings);
            return _mapper.Map<List<ProductRatingDto>>(productRatings.ToList());
        }

        public async Task<ProductRatingDto> AddRatingAsync(CreateProductRatingDto createProductRatingDto)
        {
            ProductRating productRating = _mapper.Map<ProductRating>(createProductRatingDto);
            productRating = await _baseRepository.AddAsync(productRating);
            return _mapper.Map<ProductRatingDto>(productRating);
        }

        public async Task<List<ProductRatingDto>> GetListProductRatingByProductIdAsync(Guid productId)
        {
            var query = _baseRepository.Entities;
            query = query.Include(pr => pr.OrderItem)
                .Where(o => o.OrderItem.ProductId == productId)
                .Where(p => p.IsRated == true)
                ;

            return _mapper.Map<List<ProductRatingDto>>(await query.ToListAsync());
        }

        public async Task<ProductRatingDto> GetProductRatingAsync(Guid id)
        {
            var query = _baseRepository.Entities;
            query = query.Where(x => x.Id == id).Include(p => p.OrderItem).ThenInclude(o => o.Product).ThenInclude(p => p.ProductPictures);
            return _mapper.Map<ProductRatingDto>(await query.FirstOrDefaultAsync());
        }

        public async Task<int> GetTotalRatingByUserId(string userId)
        {
            return await _baseRepository.Entities.Where(pr => pr.CreatedBy == userId).Where(pr => pr.IsRated).CountAsync();
        }

        public async Task RatingAsync(UpdateProductRatingDto updateProductRatingDto)
        {
            var productRating = await _baseRepository.GetByIdAsync(updateProductRatingDto.Id);
            productRating.Rating = updateProductRatingDto.Rating;
            productRating.Comment = updateProductRatingDto.Comment;
            productRating.IsRated = updateProductRatingDto.IsRated;
            productRating.UpdatedDate = updateProductRatingDto.UpdatedDate;
            await _baseRepository.UpdateAsync(productRating);
        }

        public async Task RemoveRatingAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }
    }
}
