using AutoMapper;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
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

        public ProductRatingService(IBaseRepository<ProductRating> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
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

        public async Task RatingAsync(UpdateProductRatingDto updateProductRatingDto)
        {
            var productRating = _mapper.Map<ProductRating>(updateProductRatingDto);
            await _baseRepository.UpdateAsync(productRating);
        }

        public async Task RemoveRatingAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }
    }
}
