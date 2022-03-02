using AutoMapper;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.MetaShop.Business;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Constants;
using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Admin.Controllers
{
    [Route(Endpoints.Product)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductPictureService _productPictureService;

        public ProductController(IProductService productService, IProductPictureService productPictureService)
        {
            _productService = productService;
            _productPictureService = productPictureService;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var ProductDto = await _productService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(ProductDto, nameof(ProductDto));
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetByIdAsync(Guid id)
            => await _productService.GetByIdAsync(id);

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAsync()
            => await _productService.GetAllAsync();


        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] CreateProductDto newProductDto)
        {
            Ensure.Any.IsNotNull(newProductDto, nameof(newProductDto));

            var asset = await _productService.AddAsync(newProductDto);
            return Created(Endpoints.Product, asset);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] ProductDto ProductDto)
        {
            Ensure.Any.IsNotNull(ProductDto, nameof(ProductDto));
            await _productService.UpdateAsync(ProductDto);

            return NoContent();
        }

        [HttpGet("find")]
        public async Task<PagedResponseModel<ProductDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _productService.PagedQueryAsync(name, page, limit);

        [HttpPost("picture")]
        public async Task<ActionResult<ProductPictureDto>> CreateProductPictureAsync([FromBody] CreateProductPictureDto newProductPictureDto)
        {
            Ensure.Any.IsNotNull(newProductPictureDto, nameof(newProductPictureDto));

            var asset = await _productPictureService.AddAsync(newProductPictureDto);
            return Created(Endpoints.Product, asset);
        }


    }
}
