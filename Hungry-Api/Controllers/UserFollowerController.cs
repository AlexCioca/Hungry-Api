using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Hungry_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserFollowerController:ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public UserFollowerController(IMapper mapper, IUnitOfWork unitOfWork, AuthService authService)
        {
            Mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("AddFolower")]
        public async Task<IActionResult> AddFolower(int followerId)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var userFollower = new UserFollowerDTO();
                userFollower.CurrentUserId = followerId;
                userFollower.FollowerId = int.Parse(userId);
                var mappedFollow = Mapper.Map<UserFollowerDTO, UserFollower>(userFollower);
                await _unitOfWork.UserFollowerRepository.AddAsync(mappedFollow);
                await _unitOfWork.CompleteAsync();
                return Ok(userFollower);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("RemoveFollowed")]
        public async Task<IActionResult> RemoveFollowed(int followedId)
        {
            try
            {
               
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
                await _unitOfWork.UserFollowerRepository.DeleteByIdAndFollowerId(followedId, int.Parse(userId));
                await _unitOfWork.CompleteAsync();
                return Ok(followedId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
