using Rookie.Ecom.MetaShop.Contracts.Dtos.Product;
using System;
using System.Collections.Generic;

namespace Rookie.Ecom.MetaShop.Contracts
{
    public class PagedResponseModel<TModel>
    {
        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<TModel> Items { get; set; }

    }
}