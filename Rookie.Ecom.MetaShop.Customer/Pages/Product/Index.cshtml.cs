using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using Rookie.Ecom.MetaShop.Customer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductRatingService _productRatingService;

        public ProductDto Product { get; set; }
        public List<ProductDto> Products { get; set; }

        public int ProductQty { get; set; } = 1;

        public List<ProductItemCartDto> Cart { get; set; }
        public List<ProductRatingDto> Reviews { get; set; }

        public IndexModel(IProductService productService, IProductRatingService productRatingService)
        {
            _productService = productService;
            _productRatingService = productRatingService;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _productService.GetByIdAsync(id);
            Reviews = await _productRatingService.GetListProductRatingByProductIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            Products = await _productService.GetRelatedProducts(Product.CategoryId, 4);
            return Page();
        }

        public async Task<ActionResult> OnPostBuyNow(Guid id, int ProductQty)
        {

            Product = await _productService.GetByIdAsync(id);
            if (Product != null)
            {
                Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
                if (Cart == null)
                {
                    Cart = new List<ProductItemCartDto>
                    {
                        new ProductItemCartDto
                        {
                            Product = Product,
                            Quantity = ProductQty
                        }
                    };
                }
                else
                {
                    int index = Exists(Cart, Product.Id);
                    if (index == -1)
                    {
                        Cart.Add(new ProductItemCartDto
                        {
                            Product = Product,
                            Quantity = ProductQty
                        });
                    }
                    else
                    {
                        Cart[index].Quantity += ProductQty;
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
                TempData["AlertMessage"] = "Product addded to cart";
                return RedirectToPage($"/Product/Index");
            }
            return NotFound();
        }
        private static int Exists(List<ProductItemCartDto> cart, Guid productId)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == productId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
