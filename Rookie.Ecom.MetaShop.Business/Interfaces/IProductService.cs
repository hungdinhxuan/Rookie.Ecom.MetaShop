using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<PagedResponseModel<ProductDto>> PagedQueryAsync(string name, int? page, int limit);

        Task<ProductDto> GetByIdAsync(Guid id);

        Task<ProductDto> GetByNameAsync(string name);

        Task<ProductDto> AddAsync(CreateProductDto ProductDto);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(UpdateProductDto ProductDto);
        Task SoftDeleteAsync(Guid id);

        Task<List<ProductDto>> GetRelatedProducts(Guid categroyId, int num);

        Task<List<ProductDto>> FilterProducts(bool isLastest, bool isFeatured, int num);
    }
}
