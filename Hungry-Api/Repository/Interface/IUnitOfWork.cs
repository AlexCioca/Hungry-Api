namespace Hungry_Api.Repository.Interface
{
    public interface IUnitOfWork:IDisposable,IAsyncDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        ILikeRepository LikeRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IRecipeCategoryRepository RecipeCategoryRepository { get; }
        IRecipeReviewRepository RecipeReviewRepository { get; }
        IUserRepository UserRepository { get; }
        IRecipeImageRepository RecipeImageRepository { get; }
        Task CompleteAsync();
    }
}
