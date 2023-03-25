using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hungry_Api.DbModels
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string? AccountId { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        
        public ICollection<Like> Categories { get;}=new List<Like>();
        public ICollection<Recipe> Recipes { get;} = new List<Recipe>();
        public ICollection<RecipeReview> RecipeReviews { get; }=new List<RecipeReview>();
        public ICollection<UserRecipe> UserRecipes { get; } = new List<UserRecipe>();

    } 
}
