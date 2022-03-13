using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using System;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile.History.Order
{
    public class ProductRatingModel : PageModel
    {
        private readonly IOrderItemService _orderItemService;

        public ProductRatingModel(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        public OrderItemDto OrderItem { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            OrderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            return Page();
        }
    }
}
