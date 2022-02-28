using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Category;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;

namespace Rookie.Ecom.MetaShop.Business
{
    public class AutoMapperProfile : AutoMapper.Profile
    {

        public AutoMapperProfile()
        {
            // ignore error from unit test
            AllowNullDestinationValues = true;

            FromDataAccessorLayer();
            FromPresentationLayer();
        }

        private void FromPresentationLayer()
        {
            CreateMap<CategoryDto, Category>(memberList: AutoMapper.MemberList.None);
            CreateMap<CreateCategoryDto, Category>(memberList: AutoMapper.MemberList.None);

            CreateMap<UpdateCategoryDto, Category>(memberList: AutoMapper.MemberList.None);


            CreateMap<ProductDto, Category>(memberList: AutoMapper.MemberList.None);
            CreateMap<CreateProductDto, Product>(memberList: AutoMapper.MemberList.None);

        }

        private void FromDataAccessorLayer()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}
