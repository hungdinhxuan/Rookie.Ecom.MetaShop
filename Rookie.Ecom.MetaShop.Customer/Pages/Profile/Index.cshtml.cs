using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using System;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;

        public MetaIdentityUserDto CurrentUser { get; set; }
        public IndexModel(IMetaIdentityUserService metaIdentityUserService)
        {
            _metaIdentityUserService = metaIdentityUserService;
        }
        public async Task<IActionResult> OnGet(string id)
        {
            CurrentUser = await _metaIdentityUserService.GetById(id);
            if (CurrentUser == null)
                return RedirectToPage("/Home/Index");
            return Page();
        }
    }
}
