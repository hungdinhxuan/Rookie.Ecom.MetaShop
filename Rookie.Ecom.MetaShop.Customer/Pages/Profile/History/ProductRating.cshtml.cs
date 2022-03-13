using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile.History
{
    public class ProductRatingModel : PageModel
    {
        public void OnGet()
        {
        }

        

        public IActionResult OnPost(float Rating)
        {
            Console.WriteLine(Rating);
            return Page();
        }
    }
}
