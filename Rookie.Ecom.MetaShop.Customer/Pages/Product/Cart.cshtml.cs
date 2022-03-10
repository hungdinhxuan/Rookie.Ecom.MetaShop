using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using Rookie.Ecom.MetaShop.Customer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rookie.Ecom.MetaShop.Customer.Pages.Product
{

    public class CartModel : PageModel
    {


        public List<ProductItemCartDto> Cart { get; set; }
        public decimal Total { get; set; }



        public void OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
            if (Cart != null)
                Total = Cart.Sum(i => i.Product.Price * i.Quantity);
            else
                Total = 0;
        }

        public IActionResult OnGetDelete(Guid productId)
        {
            Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
            int index = Exists(Cart, productId);
            if (index != -1)
                Cart.RemoveAt(index);

            if (Cart.Count > 0)
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            else
                SessionHelper.Remove(HttpContext.Session, "cart");
            return Page();
        }

        public IActionResult OnPostUpdateCart(int[] quantities)
        {
            Cart = SessionHelper.GetObjectFromJson<List<ProductItemCartDto>>(HttpContext.Session, "cart");
            if (Cart != null)
            {
                for (var i = 0; i < Cart.Count; i++)
                {
                    Cart[i].Quantity = quantities[i];
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            return Page();
        }

        private int Exists(List<ProductItemCartDto> cart, Guid productId)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == productId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
