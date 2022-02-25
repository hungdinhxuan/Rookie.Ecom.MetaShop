using AutoMapper;
using FluentAssertions;
using Rookie.Ecom.MetaShop.Business;
using System;
using Xunit;

namespace Rookie.Ecom.MetaShop.UnitTests.Business
{
    public class AutoMapperProfileShould
    {
        [Fact]
        public void BeValid()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            IMapper mapper = config.CreateMapper();

            // Act
            Action act = () => mapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Assert
            act.Should().NotThrow();
        }
    }
}
