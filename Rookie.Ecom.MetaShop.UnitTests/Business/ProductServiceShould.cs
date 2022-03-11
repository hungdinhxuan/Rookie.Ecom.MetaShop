using AutoMapper;
using FluentAssertions;
using Moq;
using Rookie.Ecom.MetaShop.Business;
using Rookie.Ecom.MetaShop.Business.Services;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
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
    public class ProductServiceShould
    {
        private readonly ProductService _productService;
        private readonly Mock<IBaseRepository<Product>> _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceShould()
        {
            _productRepository = new Mock<IBaseRepository<Product>>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();

            _productService = new ProductService(
                    _productRepository.Object,
                    _mapper
                );
        }

        [Fact]
        public async Task AddProductShouldThrowExceptionAsync()
        {
            Func<Task> act = async () => await _productService.AddAsync(null);
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddProductShouldBeSuccessfullyAsync()
        {
            var category = new Category()
            {
                Name = "abc",
                Desc = "xyz",
                ImageUrl = "gaga"
            };

            var product = new Product()
            {
                Name = "product",
                ShortDesc = "ProductShortDesc>",
                LongDesc = "long>",
                Price = 20,
                Quantity = 1,
                CategoryId = category.Id
            };

            var newProductDto = new CreateProductDto()
            {
                Name = "product 2",
                ShortDesc = "ProductShortDesc>",
                LongDesc = "long>",
                Price = 20,
                Quantity = 100,
                CategoryId = category.Id
            };
            _productRepository.Setup(x => x
                .GetByAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .Returns(Task.FromResult<Product>(product));

            _productRepository.Setup(x => x.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult(product));

            var result = await _productService.AddAsync(newProductDto);

            result.Should().NotBeNull();

            _productRepository.Verify(mock => mock.AddAsync(It.IsAny<Product>()), Times.Once());
        }


    }
}
