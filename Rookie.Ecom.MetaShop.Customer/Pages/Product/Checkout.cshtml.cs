using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Customer.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Product
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;

        public CheckoutModel(IMetaIdentityUserService metaIdentityUserService)
        {
            _metaIdentityUserService = metaIdentityUserService;
        }

        public List<ProductItemCartDto> Cart { get; set; }
        public MetaIdentityUserDto CurrentUser { get; set; }

        public decimal TotalMoney { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
            CurrentUser = await _metaIdentityUserService.GetById(User.Claims.FirstOrDefault(u => u.Type == "sub").Value);
            
            if (Cart == null)
                return RedirectToPage("/Home/Index");
            TotalMoney = Cart.Sum(p => p.Quantity * p.Product.Price);

            return Page();
        }
    }
}
