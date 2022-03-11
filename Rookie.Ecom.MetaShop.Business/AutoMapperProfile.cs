using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Category;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.Identity.Data;

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
            // Category
            CreateMap<CategoryDto, Category>(memberList: AutoMapper.MemberList.None);
            CreateMap<CreateCategoryDto, Category>(memberList: AutoMapper.MemberList.None);
            CreateMap<UpdateCategoryDto, Category>(memberList: AutoMapper.MemberList.None);


            // Prodcut
            CreateMap<ProductDto, Product>(memberList: AutoMapper.MemberList.None);
            CreateMap<CreateProductDto, Product>(memberList: AutoMapper.MemberList.None);
            CreateMap<UpdateProductDto, Product>(memberList: AutoMapper.MemberList.None);


            // Product picture
            CreateMap<CreateProductPictureDto, ProductPicture>(memberList: AutoMapper.MemberList.None);
            CreateMap<ProductPictureDto, ProductPicture>(memberList: AutoMapper.MemberList.None);


            // Order
            CreateMap<CreateOrderDto, Order>(memberList: AutoMapper.MemberList.None);
            CreateMap<OrderDto, Order>(memberList: AutoMapper.MemberList.None);


            // OrderItem
            CreateMap<CreateOrderItemDto, OrderItem>(memberList: AutoMapper.MemberList.None);
            CreateMap<OrderItemDto, OrderItem>(memberList: AutoMapper.MemberList.None);

            // Product Rating
            CreateMap<CreateProductRatingDto, ProductRating>(memberList: AutoMapper.MemberList.None);
            CreateMap<ProductRatingDto, ProductRating>(memberList: AutoMapper.MemberList.None);
            CreateMap<UpdateProductRatingDto, ProductRating>(memberList: AutoMapper.MemberList.None);
        }

        private void FromDataAccessorLayer()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductPicture, ProductPictureDto>();
            CreateMap<MetaIdentityUser, MetaIdentityUserDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<ProductRating, ProductRatingDto>();
        }
    }
}
