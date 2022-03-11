using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Constants;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductPicture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Admin.Controllers
{

    [Route(Endpoints.Product)]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
            for (int i = 0; i < newProductDto.ProductPictureDtos.Count; i++)
            {
                newProductDto.ProductPictureDtos[i].ProductId = asset.Id;
            }

            var productPictures = await _productPictureService.AddRangeAsync(newProductDto.ProductPictureDtos);

            foreach (var item in productPictures)
            {
                asset.ProductPictures.Add(item);
            }
            return Created(Endpoints.Product, asset);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateProductDto ProductDto)
        {
            Ensure.Any.IsNotNull(ProductDto, nameof(ProductDto));
            await _productService.UpdateAsync(ProductDto);
            if (ProductDto.NewProductPictureDtos != null && ProductDto.NewProductPictureDtos.Count > 0)
            {
                // delete old pictures
                if (ProductDto.ProductPictureDtos != null && ProductDto.ProductPictureDtos.Count > 0)
                    await _productPictureService.RemoveRangeAsync(ProductDto.ProductPictureDtos);
                // add new pictures
                for (int i = 0; i < ProductDto.NewProductPictureDtos.Count; i++)
                {
                    ProductDto.NewProductPictureDtos[i].ProductId = ProductDto.Id;
                }
                await _productPictureService.AddRangeAsync(ProductDto.NewProductPictureDtos);
            }
            return NoContent();
        }

        [HttpGet("find")]
        public async Task<PagedResponseModel<ProductDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _productService.PagedQueryAsync(name, page, limit);


        [HttpPost("picture")]
        public async Task<ActionResult<IEnumerable<ProductPictureDto>>> CreateProductPictureRangeAsync([FromBody] IEnumerable<CreateProductPictureDto> newProductPictureDtos)
        {
            Ensure.Any.IsNotNull(newProductPictureDtos, nameof(newProductPictureDtos));

            var asset = await _productPictureService.AddRangeAsync(newProductPictureDtos);
            return Created(Endpoints.Product, asset);
        }

        [HttpDelete("{id}/soft")]
        public async Task<ActionResult> SoftDeleteAssetAsync([FromRoute] Guid id)
        {
            var categoryDto = await _productService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(categoryDto, nameof(categoryDto));
            await _productService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
