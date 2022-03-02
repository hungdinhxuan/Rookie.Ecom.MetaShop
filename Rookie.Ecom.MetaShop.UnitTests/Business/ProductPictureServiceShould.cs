using AutoMapper;
using FluentAssertions;
using Moq;
using Rookie.Ecom.MetaShop.Business;
using Rookie.Ecom.MetaShop.Business.Services;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using Rookie.Ecom.MetaShop.DataAccessor.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

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

        [Fact]
        public async Task AddRangeShouldThrowExceptionAsync()
        {
            Func<Task> act = async () => await _productPictureService.AddRangeAsync(null);
            await act.Should().ThrowAsync<ArgumentNullException>();
        }
        /*
                [Fact]
                public async Task AddRangeShouldBeSuccessfullyAsync()
                {

                }*/
    }
}
