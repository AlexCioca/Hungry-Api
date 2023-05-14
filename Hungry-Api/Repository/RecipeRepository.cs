using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hungry_Api.Repository
{
    public class RecipeRepository:BaseRepository<Recipe>,IRecipeRepository
    {
        public RecipeRepository(HungryDbContext context) : base(context) { }

        public Task<Recipe> GetRecipeById(int id)
        {
            var recipe = _dbSet.SingleAsync(r => r.RecipeId== id);
            return recipe;
        }
        public async Task<ICollection<Recipe>> GetRecipesForUser(int id)
        {
            var recipes=  _dbSet.Where(data=>data.UserId==id).ToList();

            return recipes;
        }
        public async Task<ICollection<string>> GetAllRecipesNames()
        {
            var recipesName = _dbSet.Select(data=>data.Name).ToList();
            return recipesName;
        }
        public async Task<ICollection<Recipe>> GetRecipeSearch(string search)
        {

            var recipeSearch = _dbSet.Where(recipe=> recipe.Name.ToUpper().StartsWith(search.ToUpper())).ToList();

            return recipeSearch;
        }

        public async Task<ICollection<Recipe>> GetRecipeBasedOnCategory(int? categoryId, string recipeName)
        {
            
            if (!recipeName.IsNullOrEmpty() && categoryId!=null)
            {
                 return _dbSet.Where(r => r.Categories.Where(rc => rc.Category.CategoryId == categoryId).Any() && r.Name.ToUpper().StartsWith(recipeName.ToUpper())).ToList();
            }
           else if(!recipeName.IsNullOrEmpty() && categoryId == null)
            {
                return _dbSet.Where(r=>r.Name.ToUpper().StartsWith(recipeName.ToUpper())).ToList();
            }
            else if(recipeName.IsNullOrEmpty() && categoryId == null)
            {
                return _dbSet.ToList();
            }
            else
            {
                return _dbSet.Where(r => r.Categories.Where(rc => rc.Category.CategoryId == categoryId).Any()).ToList();
            }
               


        }

        public async Task<ICollection<Recipe>> GetRecipesOnFolloewdScroll(User user, int number)
        {
              var followed = _context.UserFollower.Where(data => data.FollowerId == user.UserId).ToList();
              ICollection<Recipe> recipes = new List<Recipe>();
            foreach(var follower in followed)
            {
                var data = _dbSet.Where(x => x.UserId == follower.CurrentUserId).Skip(number).Take(number + 15);
                foreach(var recipe in data) {

                    recipes.Add(recipe);
                }
                
            }

           return recipes;
        }

        public async Task<ICollection<Recipe>> GetLikedRecipesForUser(int userId)
        {
            var recipes = _context.UserRecipe
                  .Where(ur => ur.UserId == userId)
                  .Select(ur => ur.Recipe)
                  .ToList();

            return recipes;
        }

        public async Task<ICollection<Recipe>> GetMostLikedRecipes()
        {
            var topRecipes = _context.UserRecipe
            .GroupBy(ur => ur.RecipeId)
            .OrderByDescending(g => g.Count())
            .Take(20)
            .Select(g => g.Key)
            .ToList();

            var recipes = _context.Recipes
                .Include(r => r.User)
                .Include(r => r.Categories)
                .Where(r => topRecipes.Contains(r.RecipeId))
                .ToList();

            return recipes;
        }

        public async Task<ICollection<Recipe>> GetRecomandedRecipes(int userId)
        {
            //This query first retrieves the IDs of the categories that the user has liked, then finds recipes that are associated with any of those categories. For each of those recipes, it calculates a score based on the average rating of its reviews (if any exist). Finally, it orders the recipes by score and takes the top 10.
            /*   var userLikedCategories = _context.Likes
               .Where(l => l.UserId == userId)
               .Select(l => l.CategoryId);

               var recommendedRecipes = _context.Recipes
                   .Where(r => r.Categories.Any(rc => userLikedCategories.Contains(rc.CategoryId)))
                   .Select(r => new {
                       Recipe = r,
                       Score = r.RecipeReviews.Any() ? r.RecipeReviews.Average(rr => rr.Rating) : 0
                   })
                   .OrderByDescending(r => r.Score)
                   .Select(r => r.Recipe)
                   .Take(10)
                   .ToList();
               return recommendedRecipes;*/

            var userLikedCategories = _context.Likes
            .Where(l => l.UserId == userId)
            .Select(l => l.CategoryId);

            var recommendedRecipes = _context.Recipes
                .Where(r => r.Categories.Any(rc => userLikedCategories.Contains(rc.CategoryId))
                            && !r.UserRecipes.Any(ur => ur.UserId == userId))
                .Select(r => new {
                    Recipe = r,
                    Score = r.RecipeReviews.Any() ? r.RecipeReviews.Average(rr => rr.Rating) : 0
                })
                .OrderByDescending(r => r.Score)
                .Select(r => r.Recipe)
                .Take(10)
                .ToList();
            return recommendedRecipes;

        }
        public async Task<ICollection<Recipe>> GetBestRatingReviews()
        {
            var topRatedRecipes = _context.Recipes
            .Select(r => new
            {
            Recipe = r,
            AverageRating = r.RecipeReviews.Average(rr => rr.Rating)
            })
            .OrderByDescending(r => r.AverageRating)
            .Select(r => r.Recipe)
            .Take(10)
            .ToList();
            return topRatedRecipes;
        }
    }
}
