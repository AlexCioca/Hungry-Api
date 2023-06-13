using Hungry_Api.DbModels;

namespace Hungry_Api.DTO
{
    public class RecipeDTO
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string? MainPhoto { get; set; }
        public int? PreparationTime { get; set; }
        public string Difficulty { get; set; }
        public int? Serves { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
