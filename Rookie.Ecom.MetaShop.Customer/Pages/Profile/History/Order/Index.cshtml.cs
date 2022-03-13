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
        private readonly IProductService _productService;

        public IndexModel(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        public List<OrderDto> Orders { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Guid UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value);
            Orders = await _orderService.GetListOrderByUserIdAsync(UserId);
            if (Orders != null)
            {
                for (int i = 0; i < Orders.Count; i++)
                {
                    for (int j = 0; j < Orders[i].OrderItems.Count; j++)
                    {
                        Orders[i].OrderItems[j].Product = await _productService.GetByIdAsync(Orders[i].OrderItems[j].ProductId);
                    }
                }
            }
            else
            {
                Orders = new List<OrderDto>();
            }
            return Page();
        }
    }
}
