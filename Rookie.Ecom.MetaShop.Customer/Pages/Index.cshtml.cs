using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;


        public IndexModel(ILogger<IndexModel> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
        }


        public PagedResponseModel<ProductDto> Products { get; set; }

        public List<CategoryDto> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public string UserNameIdentity { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Products = await _productService.PagedQueryAsync(null, CurrentPage, PageSize);
            Categories = (List<CategoryDto>)await _categoryService.GetAllAsync();
            UserNameIdentity = User.Identity.Name;
            return Page();
        }
    }
}
