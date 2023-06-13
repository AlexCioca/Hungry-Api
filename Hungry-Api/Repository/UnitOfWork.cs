using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Microsoft.Identity.Client;

namespace Hungry_Api.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly HungryDbContext _context;

        private ICategoryRepository? _categoryRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

        private IIngredientRepository? _ingredientRepository;
        public IIngredientRepository IngredientRepository => _ingredientRepository ??= new IngredientRepository(_context);

        private ILikeRepository? _likeRepository;
        public ILikeRepository LikeRepository => _likeRepository ??= new LikeRepository(_context);

        private IRecipeCategoryRepository? _recipeCategoryRepository;
        public IRecipeCategoryRepository RecipeCategoryRepository => _recipeCategoryRepository ??= new RecipeCategoryRepository(_context);

        private IRecipeRepository? _recipeRepository;
        public IRecipeRepository RecipeRepository => _recipeRepository??= new RecipeRepository(_context);

        private IRecipeReviewRepository? _recipeReviewRepository;
        public IRecipeReviewRepository RecipeReviewRepository => _recipeReviewRepository??=new RecipeReviewRepository(_context);

        private IUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository??= new UserRepository(_context);
        
        private IRecipeImageRepository? _recipeImageRepository;
        public IRecipeImageRepository RecipeImageRepository => _recipeImageRepository??=new RecipeImageRepository(_context);
        
        private IRecipeStepsRepository? _recipeStepsRepository;
        public IRecipeStepsRepository RecipeStepsRepository=>_recipeStepsRepository??=new RecipeStepsRepository(_context); 

        private IUserFollowerRepository? _userFollowerRepository;
        public IUserFollowerRepository UserFollowerRepository=> _userFollowerRepository??=new UserFollowerRepository(_context);

        private IUserRecipeRepository? _userRecipeRepository;
        public IUserRecipeRepository UserRecipeRepository=> _userRecipeRepository??=new UserRecipeRepository(_context);
        
        private IMessageRepository? _messageRepository;
        public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context);

        private ITicketRepository? _ticketRepository;
        public ITicketRepository TicketRepository => _ticketRepository ??= new TicketRepository(_context);

        public UnitOfWork(HungryDbContext context)
        {
            _context=context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }

                _disposed = true;
            }
        }
    }
}
