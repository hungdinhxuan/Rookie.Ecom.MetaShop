using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.ProductRating;
using System;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile.History.Order
{
    public class ProductRatingModel : PageModel
    {
        private readonly IProductRatingService _productRatingService;


        public ProductRatingModel(IProductRatingService productRatingService)
        {
            _productRatingService = productRatingService;

        }
        public ProductRatingDto ProductRating { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            ProductRating = await _productRatingService.GetProductRatingAsync(id);
            if (ProductRating == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Guid id, float Rating, string Comment)
        {
            ProductRating = await _productRatingService.GetProductRatingAsync(id);
            if (ProductRating == null)
            {
                return NotFound();
            }
            if (!ProductRating.IsRated)
            {
                UpdateProductRatingDto updateProductRatingDto = new UpdateProductRatingDto()
                {
                    Id = id,
                    Rating = Rating,
                    Comment = Comment,
                    UpdatedDate = DateTime.Now,
                    IsRated = true
                };
                await _productRatingService.RatingAsync(updateProductRatingDto);
                TempData["AlertMessage"] = "Rating Product Successfully!";
            }

            return RedirectToPage($"/Profile/History/Order/{ProductRating.OrderItem.OrderId}");
        }
    }
}
