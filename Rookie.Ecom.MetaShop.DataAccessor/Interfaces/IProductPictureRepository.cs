using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.DataAccessor.Interfaces
{
    public interface IProductPictureRepository : IBaseRepository<ProductPicture>
    {
        Task<IEnumerable<ProductPictureDto>> GetAllByProductIdAsync(Guid productId);
    }
}
