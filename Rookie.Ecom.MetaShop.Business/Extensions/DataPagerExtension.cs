using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Business.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            int page,
            int limit)
            where TModel : class
        {

            var paged = new PagedModel<TModel>();

            page = (page <= 0) ? 1 : page;

            paged.CurrentPage = page;

            paged.PageSize = limit;

            var startRow = (page - 1) * limit;

            paged.Items = await query
                        .Skip(startRow)
                        .Take(limit)
                        .ToListAsync();

            paged.TotalItems = await query.CountAsync();
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}
