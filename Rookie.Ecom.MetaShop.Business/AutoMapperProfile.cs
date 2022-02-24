using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;

namespace Rookie.Ecom.MetaShop.Business
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            FromDataAccessorLayer();
            FromPresentationLayer();
        }

        private void FromPresentationLayer()
        {
            CreateMap<CategoryDto, Category>();

        }

        private void FromDataAccessorLayer()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
