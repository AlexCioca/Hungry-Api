using Hungry_Api.DbModels;

namespace Hungry_Api.DTO
{
    public class IngredientDTO
    {
        public int IngredientsId { get; set; }
        public string IngredientsName { get; set; }
        public int RecipeId { get; set; }
    }
}
