using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Interfaces
{
    public interface IProductRatingService
    {
        Task<ProductRatingDto> AddRatingAsync(CreateProductRatingDto createProductRatingDto);

        Task<List<ProductRatingDto>> AddRangeProductRatingAsync(List<CreateProductRatingDto> createProductRatingDtos);
        Task RatingAsync(UpdateProductRatingDto updateProductRatingDto);

        Task RemoveRatingAsync(Guid id);

        Task<ProductRatingDto> GetProductRatingAsync(Guid id);

        Task<List<ProductRatingDto>> GetListProductRatingByProductIdAsync(Guid productId);


    }
}
