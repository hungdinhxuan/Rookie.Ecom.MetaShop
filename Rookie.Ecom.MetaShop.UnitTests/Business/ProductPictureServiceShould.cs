using AutoMapper;
using Moq;
using Rookie.Ecom.MetaShop.Business;
using Rookie.Ecom.MetaShop.Business.Services;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;

namespace Rookie.Ecom.UnitTests.Business
{
    /// <summary>
    /// https://codewithshadman.com/repository-pattern-csharp/
    /// https://dotnettutorials.net/lesson/generic-repository-pattern-csharp-mvc/
    /// https://fluentassertions.com/exceptions/
    /// https://stackoverflow.com/questions/37422476/moq-expression-with-constraint-it-isexpressionfunct-bool
    /// </summary>
    public class ProductPicturePictureServiceShould
    {
        private readonly ProductPictureService _productPictureService;
        private readonly Mock<IBaseRepository<ProductPicture>> _productPictureRepository;
        private readonly IMapper _mapper;

        public ProductPicturePictureServiceShould()
        {
            _productPictureRepository = new Mock<IBaseRepository<ProductPicture>>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _productPictureService = new ProductPictureService(
                    _productPictureRepository.Object,
                    _mapper
                );
        }
    }
}
