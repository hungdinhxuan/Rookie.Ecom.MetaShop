using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;

        public MetaIdentityUserDto CurrentUser { get; set; }
        public IndexModel(IMetaIdentityUserService metaIdentityUserService)
        {
            _metaIdentityUserService = metaIdentityUserService;
        }
        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _metaIdentityUserService.GetById(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
            if (CurrentUser == null)
                return RedirectToPage("/Home/Index");
            /*if (id != User.Claims.FirstOrDefault(c => c.Type == "sub").Value)
                return RedirectToPage("/Errors/Error403");*/
            return Page();
        }
    }
}
