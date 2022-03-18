namespace Rookie.Ecom.MetaShop.Contracts.Dtos.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public string ImageUrl { get; set; }

        public string? CreatedBy { get; set; }

    }
}
