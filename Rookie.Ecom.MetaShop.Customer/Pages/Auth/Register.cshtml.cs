using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Auth
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;

        [BindProperty]
        public UserRegistrationDto UserRegistration { get; set; }

        public RegisterModel(IMetaIdentityUserService metaIdentityUserService)
        {

            UserRegistration = new UserRegistrationDto();
            _metaIdentityUserService = metaIdentityUserService;
        }
        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Home/Login");
            return Page();
        }


        public async Task<IActionResult> OnPostRegister()
        {
            if (ModelState.IsValid)
            {
                var result = await _metaIdentityUserService.Register(UserRegistration, "Customer");


                if (result.Succeeded)
                {
                    TempData["AlertMessage"] = "Register Successfully";
                    return RedirectToPage("/Auth/Login");
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("message", error.Description);
                        }
                    }

                }

            }
            return Page();
        }
    }
}
