using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts;
using Rookie.Ecom.MetaShop.Contracts.Dtos;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Customer.Helpers;
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
            TotalCartItem = 0;
        }


        public PagedResponseModel<ProductDto> Products { get; set; }

        public List<ProductDto> FeaturedProducts { get; set; }
        public List<ProductDto> LastestProducts { get; set; }

        public List<CategoryDto> Categories { get; set; }

        public int PageSize { get; set; } = 5;

        public string CurrentFilter { get; set; }

        public int TotalCartItem { get; set; }

        public int CurrentPage { get; set; } = 1;

        public Guid? SelectedCategoryId { get; set; }


        public async Task OnGet(string searchString, int? pageIndex, Guid? categoryId)
        {
            CurrentPage = pageIndex ?? 1;


            CurrentFilter = searchString;
            SelectedCategoryId = categoryId;

            Products = await _productService.PagedQueryAsync(CurrentFilter, SelectedCategoryId, CurrentPage, PageSize);
            LastestProducts = await _productService.FilterProducts(true, false, 12);
            FeaturedProducts = await _productService.FilterProducts(false, true, 6);
            Categories = (await _categoryService.GetAllAsync()).ToList();
        }
    }
}
