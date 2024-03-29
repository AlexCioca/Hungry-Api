﻿using Hungry_Api.DbModels;

namespace Hungry_Api.Repository.Interface
{
    public interface IIngredientRepository:IBaseRepository<Ingredient>
    {
        Task<ICollection<Ingredient>> GetIngredientsForRecipe(int recipeId);
        Task AddIngredientForRecipe(Ingredient ingredient);
        Task DeleteIngredientForRecipe(Ingredient ingredient);

    }
}
