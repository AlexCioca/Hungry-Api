using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public RecipeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;
            
        }
        [HttpGet("GetAllRecipes")]
        public async Task<IActionResult> GetAllRecipe()
        {
            try
            {
                var recipes = _unitOfWork.RecipeRepository.GetAllAsync(); 

                if(recipes.Result.IsNullOrEmpty())
                {
                    return NotFound("Recipes not found");
                }

                var mapped = Mapper.Map<Recipe[], RecipeDTO[]>(recipes.Result.ToArray());

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("AddRecipe")]
        public async Task<IActionResult> AddRecipe([FromBody]RecipeDTO recipe)
        {
            try
            {
                var mapped = Mapper.Map<RecipeDTO, Recipe>(recipe);
                await _unitOfWork.RecipeRepository.AddAsync(mapped);
                await _unitOfWork.CompleteAsync();
                return Ok(recipe);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe([FromBody]RecipeDTO recipe)
        {
            try
            {
                var mapped = Mapper.Map<RecipeDTO, Recipe>(recipe);
                await _unitOfWork.RecipeRepository.UpdateAsync(mapped);
                await _unitOfWork.CompleteAsync();
                return Ok(mapped);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRecipe")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try
            {
                var recipe = _unitOfWork.RecipeRepository.GetRecipeById(id);
                await _unitOfWork.RecipeRepository.DeleteAsync(recipe.Result);
                await _unitOfWork.CompleteAsync();
                return Ok(recipe.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
