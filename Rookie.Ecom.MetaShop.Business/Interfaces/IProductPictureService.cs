using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Interfaces
{
    public interface IProductPictureService
    {
        Task<IEnumerable<ProductPictureDto>> GetAllByProductIdAsync(Guid productId);
        Task<ProductPictureDto> AddAsync(CreateProductPictureDto item);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ProductPictureDto>> AddRangeAsync(IEnumerable<CreateProductPictureDto> items);

        Task RemoveRangeAsync(IEnumerable<ProductPictureDto> productPictures);
    }
}
