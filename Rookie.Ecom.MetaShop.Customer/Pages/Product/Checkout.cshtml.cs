using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Auth;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Customer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Product
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly IMetaIdentityUserService _metaIdentityUserService;
        private readonly IOrderService _orderService;

        public CheckoutModel(IMetaIdentityUserService metaIdentityUserService, IOrderService orderService)
        {
            _metaIdentityUserService = metaIdentityUserService;
            _orderService = orderService;
        }

        public List<ProductItemCartDto> Cart { get; set; }
        public MetaIdentityUserDto CurrentUser { get; set; }

        [BindProperty]
        public CreateOrderDto CreateOrder { get; set; }
        public decimal TotalMoney { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Home/Login");

            Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
            CurrentUser = await _metaIdentityUserService.GetById(User.Claims.FirstOrDefault(u => u.Type == "sub").Value);
            CreateOrder = new CreateOrderDto
            {
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                Country = CurrentUser.Country,
                Line1 = CurrentUser.Line1,
                Line2 = CurrentUser.Line2,
                PhoneNumber = CurrentUser.PhoneNumber,
                Province = CurrentUser.Province
            };
            if (Cart == null)
                return RedirectToPage("/Home/Index");
            TotalMoney = Cart.Sum(p => p.Quantity * p.Product.Price);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
            OrderDto order = await _orderService.CreateOrder(CreateOrder);
            return Page();
        }
    }
}
