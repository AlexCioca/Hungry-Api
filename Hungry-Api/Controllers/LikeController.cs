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
    public class LikeController:ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public LikeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpPost("AddCategoryLike")]
        public async Task<IActionResult> AddCategoryLike(int categoryId)
        {
            try
            {

                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                Like like = new Like();
                like.CategoryId = categoryId;
                like.UserId = int.Parse(userId);
               
                await _unitOfWork.LikeRepository.AddAsync(like);
                await _unitOfWork.CompleteAsync();

                return Ok(like);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteCategoryLike")]
        public async Task<IActionResult> DeleteCategoryLike(int categoryId)
        {
            try
            {

                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var like = await _unitOfWork.LikeRepository.GetSingleLike(int.Parse(userId), categoryId);

                await _unitOfWork.LikeRepository.DeleteAsync(like);
                await _unitOfWork.CompleteAsync();

                return Ok(like);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
