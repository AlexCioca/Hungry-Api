using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
                var recipes = await _unitOfWork.RecipeRepository.GetAllAsync();

                var mapped = Mapper.Map<Recipe[], RecipeDTO[]>(recipes.ToArray());

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetFollowedRecipes")]
        public async Task<IActionResult> GetFollowedRecipes(int number)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var user = await _unitOfWork.UserRepository.GetUserById(int.Parse(userId));
                var recipes = await _unitOfWork.RecipeRepository.GetRecipesOnFolloewdScroll(user, number);

                var mapped = Mapper.Map<Recipe[], RecipeDTO[]>(recipes.ToArray());

                

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetReviewRating")]
        public async Task<IActionResult> GetRecipeRating(int recipeId)
        {
            try
            {
                var rating = await _unitOfWork.RecipeReviewRepository.GetRecipeRating(recipeId);
                return Ok(rating);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRecipeCategory")]
        public async Task<IActionResult> GetRecipeCategory(int recipeId)
        {
            try
            {
                var category = await _unitOfWork.RecipeCategoryRepository.GetCategoryForRecipe(recipeId);

                var mapped = Mapper.Map<Category, CategoryDTO>(category);
                return Ok(mapped);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMostLikedRecipes")]
        public async Task<IActionResult> GetMostLikedRecipes()
        {
            try
            {
                var recipes = await _unitOfWork.RecipeRepository.GetMostLikedRecipes();

                var mapped = Mapper.Map<ICollection<Recipe>, ICollection<RecipeDTO>>(recipes);

                return Ok(mapped);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetTopRatedRecipes")]
        public async Task<IActionResult> GetTopRatedRecipes()
        {
            try
            {
                var recipes = await _unitOfWork.RecipeRepository.GetBestRatingReviews();

                var mapped = Mapper.Map<ICollection<Recipe>, ICollection<RecipeDTO>>(recipes);

                return Ok(mapped);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRecomandedRecipes")]
        public async Task<IActionResult> GetRecomandedRecipes()
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var recipes = await _unitOfWork.RecipeRepository.GetRecomandedRecipes(int.Parse(userId));

                var mapped = Mapper.Map<ICollection<Recipe>, ICollection<RecipeDTO>>(recipes);

                return Ok(mapped);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetLikedRecipesForUser")]
        public async Task<IActionResult> GetLikedRecipesForUser()
        {
            try
            {

                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var recipes = await _unitOfWork.RecipeRepository.GetLikedRecipesForUser(int.Parse(userId));

                var mapped = Mapper.Map<ICollection<Recipe>, ICollection<RecipeDTO>>(recipes);

                return Ok(mapped);


            }
            catch(Exception ex) {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllRecipesNames")]
        public async Task<IActionResult> GetAllRecipeNames()
        {
            try
            {
                var recipesNames = await _unitOfWork.RecipeRepository.GetAllRecipesNames();

                return Ok(recipesNames);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetRecipeSearch")]
        public async Task<IActionResult> GetRecipeSearch(string serach)
        {
            try { 
            var recipes = await _unitOfWork.RecipeRepository.GetRecipeSearch(serach);

            if(recipes.IsNullOrEmpty())
            {
                return NotFound("Recipes not found");
            }
            var mappedRecipes = Mapper.Map<ICollection<Recipe>, ICollection<RecipeDTO>>(recipes);
                
                return Ok(mappedRecipes);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllRecipesForCurrentUser")]
        public async Task<IActionResult> GetAllRecipesForCurrentUser(int userId)
        {
            try
            {
                var recipes =await _unitOfWork.RecipeRepository.GetRecipesForUser(userId);

                var mapped = Mapper.Map<Recipe[], RecipeDTO[]>(recipes.ToArray());

                return Ok(mapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetRecipeById/{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            try
            {
                var recipe = await _unitOfWork.RecipeRepository.GetRecipeById(id);
                if (recipe == null)
                {
                    return NotFound();
                }
                var mappedRecipe = Mapper.Map<Recipe, RecipeDTO>(recipe);
                return Ok(mappedRecipe);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetRecipeBasedOnCategory")]
        public async Task<IActionResult> GetRecipeBasedOnCategory(int? categoryId,string? recipeName)
        {
            try
            {
                var recipe = await _unitOfWork.RecipeRepository.GetRecipeBasedOnCategory(categoryId, recipeName);

                var mappedRecipe = Mapper.Map<ICollection<Recipe>,ICollection<RecipeDTO>>(recipe);

                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetIngredientsForRecipe")]
        public async Task<IActionResult> GetIngredientsForRecipe(int id)
        {
            try
            {
                var ingredients = await _unitOfWork.IngredientRepository.GetIngredientsForRecipe(id);
                if(ingredients.IsNullOrEmpty())
                {
                    return NotFound();
                }
                var mappedIngredients = Mapper.Map<ICollection<Ingredient>, ICollection<IngredientDTO>>(ingredients);
                return Ok(mappedIngredients);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetStepsForRecipe")]
        public async Task<IActionResult> GetStepsForRecipe(int id)
        {
            try
            {
                var steps = await _unitOfWork.RecipeStepsRepository.GetStepsForRecipe(id);
                if(steps.IsNullOrEmpty())
                {
                    return NotFound();
                }
                var mappedSteps = Mapper.Map<ICollection<RecipeSteps>, ICollection<RecipeStepsDTO>>(steps);
                return Ok(mappedSteps);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetImagesForRecipe")]
        public async Task<IActionResult> GetImagesForRecipe(int id)
        {
            try
            {
                
                var images = await _unitOfWork.RecipeImageRepository.GetImagesForARecepie(id);

                var mappedImages = Mapper.Map<ICollection<RecipeImage>, ICollection<RecipeImageDTO>>(images);
                return Ok(mappedImages);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddPhotoForRecipe")]
        public async Task<IActionResult> AddPhotoForRecipe([FromBody]RecipeImageDTO image)
        {
            try
            {
                var mappedImage = Mapper.Map<RecipeImageDTO, RecipeImage>(image);
                await _unitOfWork.RecipeImageRepository.AddAsync(mappedImage);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedImage);

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
             

                return Ok(mapped);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPost("AddIngredient")]
        public async Task<IActionResult> AddIngredient([FromBody] IngredientDTO ingredient)
        {
            try
            {


                var mappedIngredient = Mapper.Map<IngredientDTO, Ingredient>(ingredient);
                await _unitOfWork.IngredientRepository.AddIngredientForRecipe(mappedIngredient);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedIngredient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] RecipeReviewDTO review)
        { 
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                review.UserId = int.Parse(userId);

                var mappedReview = Mapper.Map<RecipeReviewDTO, RecipeReview>(review);
                await _unitOfWork.RecipeReviewRepository.AddAsync(mappedReview);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddStep")]
        public async Task<IActionResult> AddStep([FromBody] RecipeStepsDTO step)
        {
            try
            {
                var mappedStep = Mapper.Map<RecipeStepsDTO, RecipeSteps>(step);
                await _unitOfWork.RecipeStepsRepository.AddStepForRecipe(mappedStep);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedStep);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe([FromBody] RecipeDTO recipe)
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

        [HttpPut("ChangeMainPhotoForARecipe")]
        public async Task<IActionResult> ChangeMainPhotoForARecipe([FromBody] RecipeMainPhotoDTO obj)
        {
            try
            {
               
                var recipe= await _unitOfWork.RecipeRepository.GetRecipeById(obj.RecipeId);
                recipe.MainPhoto = obj.Photo;

                await _unitOfWork.RecipeRepository.UpdateAsync(recipe);
                await _unitOfWork.CompleteAsync();


                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePhotoForRecipe")]
        public async Task<IActionResult> UpdatePhotoForRecipe(RecipeImageDTO image)
        {
            try
            {
                var mappedImage = Mapper.Map<RecipeImageDTO, RecipeImage>(image);
                await _unitOfWork.RecipeImageRepository.UpdateAsync(mappedImage);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedImage);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeletePhotoForRecipe")]
        public async Task<IActionResult> DeletePhotoForRecipe(int id)
        {
            try
            {
                var image = _unitOfWork.RecipeImageRepository.GetImageById(id);
                await _unitOfWork.RecipeImageRepository.DeleteAsync(image.Result);
                await _unitOfWork.CompleteAsync();
                return Ok(image);

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
                recipe.Result.IsDeleted = true;
                await _unitOfWork.RecipeRepository.UpdateAsync(recipe.Result);
                await _unitOfWork.CompleteAsync();
                return Ok(recipe.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteIngredient")]
        public async Task<IActionResult> DeleteIngredient([FromBody] IngredientDTO ingredient)
        {
            try
            {
                var mappedIngredient = Mapper.Map<IngredientDTO, Ingredient>(ingredient);
                await _unitOfWork.IngredientRepository.DeleteAsync(mappedIngredient);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedIngredient);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteStep")]
        public async Task<IActionResult> DeleteStep([FromBody]RecipeStepsDTO step)
        {
            try
            {
                var mappedStep = Mapper.Map<RecipeStepsDTO, RecipeSteps>(step);
                await _unitOfWork.RecipeStepsRepository.DeleteAsync(mappedStep);
                await _unitOfWork.CompleteAsync();
                return Ok(mappedStep);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReiview([FromBody]RecipeReviewDTO review)
        {
            try
            {
                var mappedReview = Mapper.Map<RecipeReviewDTO,RecipeReview>(review);
                await _unitOfWork.RecipeReviewRepository.DeleteAsync(mappedReview);
                await _unitOfWork.CompleteAsync();

                return Ok(mappedReview);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}
