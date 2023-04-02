using Hungry_Api.DbModels;

namespace Hungry_Api.DTO
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? MainPhoto { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
