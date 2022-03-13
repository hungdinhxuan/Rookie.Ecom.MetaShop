using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Order;
using System;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Profile.History.Order
{
    public class DetailModel : PageModel
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public DetailModel(IOrderService orderService, IProductService productService)
        {

            _productService = productService;
            _orderService = orderService;
        }
        public OrderDto Order { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            Order = await _orderService.GetOrderByIdAysnc(id);

            if (Order == null)
                return NotFound();
            for (int i = 0; i < Order.OrderItems.Count; i++)
            {
                Order.OrderItems[i].Product = await _productService.GetByIdAsync(Order.OrderItems[i].ProductId);
            }
            return Page();
        }
    }
}
