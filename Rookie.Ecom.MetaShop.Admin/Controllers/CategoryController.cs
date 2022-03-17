using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Constants;
using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route(Endpoints.Category)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(categoryDto, nameof(categoryDto));
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<CategoryDto> GetByIdAsync(Guid id)
            => await _categoryService.GetByIdAsync(id);

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetAsync()
            => await _categoryService.GetAllAsync();


        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateAsync([FromBody] CreateCategoryDto newCategoryDto)
        {
            Ensure.Any.IsNotNull(newCategoryDto, nameof(newCategoryDto));
            newCategoryDto.CreatedBy = User.Claims.SingleOrDefault(c => c.Type == "sub").Value;
            var asset = await _categoryService.AddAsync(newCategoryDto);
            return Created(Endpoints.Category, asset);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCategoryDto categoryDto)
        {
            Ensure.Any.IsNotNull(categoryDto, nameof(categoryDto));
            categoryDto.UpdatedBy = User.Claims.SingleOrDefault(c => c.Type == "sub").Value;
            await _categoryService.UpdateAsync(categoryDto);

            return NoContent();
        }

        [HttpGet("find")]
        public async Task<PagedResponseModel<CategoryDto>>
            FindAsync(string name, int page = 1, int limit = 10)
            => await _categoryService.PagedQueryAsync(name, page, limit);

        [HttpDelete("{id}/soft")]
        public async Task<ActionResult> SoftDeleteAssetAsync([FromRoute] Guid id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);
            Ensure.Any.IsNotNull(categoryDto, nameof(categoryDto));
            await _categoryService.SoftDeleteAsync(id);
            return NoContent();
        }

    }
}
