﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hungry_Api.DbModels
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string? MainPhoto { get; set; }
        public int? PreparationTime { get; set; }
        public string Difficulty { get; set; }
        public int? Serves { get; set; }
        public DateTime CreatedDate { get; set; } 
        public virtual User User { get; set; } = null;
        public ICollection<RecipeCategory> Categories { get;} =new List<RecipeCategory>();
        public ICollection<RecipeReview> RecipeReviews { get; } = new List<RecipeReview>();
        public ICollection<Ingredient> Ingredients { get;} = new List<Ingredient>();
        public ICollection<UserRecipe> UserRecipes { get; } = new List<UserRecipe>();
        public ICollection<RecipeImage> RecipeImages { get; } = new List<RecipeImage>();
        public ICollection<RecipeSteps> RecipeSteps { get; } = new List<RecipeSteps>();


    }
}
