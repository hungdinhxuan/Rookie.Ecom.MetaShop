using System;

namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }

        public string ImageUrl { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
