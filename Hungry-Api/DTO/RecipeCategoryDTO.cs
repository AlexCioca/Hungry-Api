using Hungry_Api.DbModels;

namespace Hungry_Api.DTO
{
    public class RecipeCategoryDTO
    {
        public int RecipeCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int RecipeId { get; set; }
    }
}
