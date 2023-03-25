using Hungry_Api.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.DTO
{
    public class UserRecipeDTO
    {
        public int UserRecipeId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
