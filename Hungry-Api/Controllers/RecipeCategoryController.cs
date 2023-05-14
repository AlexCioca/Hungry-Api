using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeCategoryController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public RecipeCategoryController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("AddRecipeCategory")]
        public async Task<IActionResult> AddRecipeCategory(int recipeId, int categoryId)
        {
            try
            {
                RecipeCategory recipeCategory = new RecipeCategory();
                recipeCategory.CategoryId= categoryId;
                recipeCategory.RecipeId=recipeId;


                await _unitOfWork.RecipeCategoryRepository.AddAsync(recipeCategory);
                await _unitOfWork.CompleteAsync();

              
                return Ok(recipeCategory);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateRecipeCategory")]
        public async Task<IActionResult> UpdateRecipeCategory(int recipeId, int categoryId)
        {
            try
            {
                var oldCategory = _unitOfWork.RecipeCategoryRepository.GetRecipeCategoryForRecipe(recipeId);
                await _unitOfWork.RecipeCategoryRepository.DeleteAsync(oldCategory.Result);
                RecipeCategory recipeCategory = new RecipeCategory();
                recipeCategory.CategoryId = categoryId;
                recipeCategory.RecipeId = recipeId;


                await _unitOfWork.RecipeCategoryRepository.AddAsync(recipeCategory);
                await _unitOfWork.CompleteAsync();


                return Ok(recipeCategory);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
