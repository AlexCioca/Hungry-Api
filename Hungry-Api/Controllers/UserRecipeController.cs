using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRecipeController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public UserRecipeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("AddLikeForRecipe")]
        public async Task<IActionResult> AddLikeForRecipe([FromBody] UserRecipeDTO userRecipe)
        {
            try
            {

                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
                var mapped = Mapper.Map<UserRecipeDTO, UserRecipe>(userRecipe);
                mapped.UserId = int.Parse(userId);

                var category = await _unitOfWork.RecipeCategoryRepository.GetCategoryForRecipe(userRecipe.RecipeId);
                Like like = new Like();
                like.CategoryId = category.CategoryId;
                like.UserId = int.Parse(userId);

                await _unitOfWork.LikeRepository.AddAsync(like);
           
                await _unitOfWork.UserRecipeRepository.AddAsync(mapped);
                await _unitOfWork.CompleteAsync();

                return Ok(mapped);

            }
            catch (Exception ex) { 
                
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("GetLikesForRecipe")]
        public async Task<IActionResult> GetLikesForRecipe(int recipeId)
        {
            try
            {
                var likeNumber = await _unitOfWork.UserRecipeRepository.GetLikesForRecipe(recipeId);

                return Ok(likeNumber);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetIfRecipeIsLiked")]
        public async Task<IActionResult> GetIfRecipeIsLiked(int recipeId)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var liked = await _unitOfWork.UserRecipeRepository.GetRecipeLike(int.Parse(userId),recipeId);

                return Ok(liked);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteLikeForRecipe")]
        public async Task<IActionResult> DeleteLikeForRecipe([FromBody] UserRecipeDTO userRecipe)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
             
                var mapped = await _unitOfWork.UserRecipeRepository.GetSingleUserRecipe(int.Parse(userId), userRecipe.RecipeId);
                var category = await _unitOfWork.RecipeCategoryRepository.GetCategoryForRecipe(userRecipe.RecipeId);
                Like like = new Like();
                like.CategoryId = category.CategoryId;
                like.UserId = int.Parse(userId);

                await _unitOfWork.LikeRepository.DeleteSingleLike(like.UserId, like.CategoryId);

                await _unitOfWork.UserRecipeRepository.DeleteAsync(mapped);
                await _unitOfWork.CompleteAsync();

                return Ok(mapped);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }


    }
}
