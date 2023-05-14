using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IRecipeStepsRepository:IBaseRepository<RecipeSteps>
    {
        Task<ICollection<RecipeSteps>> GetStepsForRecipe(int recipeId);
        Task AddStepForRecipe(RecipeSteps step);
        Task DeleteStepForRecipe(RecipeSteps step);
    }
}
