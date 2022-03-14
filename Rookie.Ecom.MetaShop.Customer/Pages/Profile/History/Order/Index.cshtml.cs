using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile.History.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;


        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public List<OrderDto> Orders { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Orders = await _orderService.GetListOrderByUserIdAsync(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
            if (Orders == null)
            {
                Orders = new List<OrderDto>();
            }
            return Page();
        }
    }
}
