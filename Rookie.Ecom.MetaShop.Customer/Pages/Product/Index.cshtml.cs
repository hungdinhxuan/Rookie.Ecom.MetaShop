using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public ProductDto Product { get; set; }
        public List<ProductDto> Products { get; set; }
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _productService.GetByIdAsync(id);

            if (Product == null)
            {
                return NotFound();
            }
            Products = await _productService.GetRelatedProducts(Product.CategoryId, 4);
            return Page();
        }
    }
}
