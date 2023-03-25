using Hungry_Api.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Hungry_Api.DTO
{
    public class RecipeReviewDTO
    {
        public int RecipeReviewId { get; set; }
        public int RecipeId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
    }
}
