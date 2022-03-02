using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using Rookie.Ecom.MetaShop.DataAccessor.Data;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.DataAccessor.Repositories
{
    public class ProductPictureRepository : BaseRepository<ProductPicture>, IProductPictureRepository
    {
        public ProductPictureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ProductPictureDto>> GetAllByProductIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
