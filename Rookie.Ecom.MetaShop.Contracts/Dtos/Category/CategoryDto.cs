using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public string ImageUrl { get; set; }


    }
}
