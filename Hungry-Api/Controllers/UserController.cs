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
    public class UserController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user= await _unitOfWork.UserRepository.GetUserById(id);
              
                var mappedUser = Mapper.Map<User, UserDTO>(user);

                return Ok(mappedUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserByToken")]
        public async Task<IActionResult> GetUserByToken()
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var u =  await _unitOfWork.UserRepository.GetUserById(int.Parse(userId));

                if (u == null)
                {
                    return NotFound();
                }

                var mappedUser = Mapper.Map<User, UserDTO>(u);
                
                return Ok(mappedUser);


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByUsername(username);
                if (user == null)
                {
                    return NotFound();
                }

                var mappedUser = Mapper.Map<User, UserDTO>(user);

                return Ok(mappedUser);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            try
            {
                var mappedUser = _unitOfWork.UserRepository.GetUserById(user.UserId).Result;
                mappedUser.Username=user.Username;
                mappedUser.FirstName=user.FirstName;
                mappedUser.LastName=user.LastName;
                mappedUser.Email=user.Email;
                mappedUser.Photo=user.Photo;
                await _unitOfWork.UserRepository.UpdateAsync(mappedUser);
                await _unitOfWork.CompleteAsync();

                return Ok(mappedUser);

            }
            catch(Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetNewUsers"),Authorize]
        public async Task<IActionResult> GetNewUsers(int number)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;


                var users = await _unitOfWork.UserRepository.GetAllAsync();
                var followers = await _unitOfWork.UserFollowerRepository.GetByFollowerAsync(int.Parse(userId));

                foreach (var follower in followers)
                {
                    var removedUser=await _unitOfWork.UserRepository.GetUserById((int)follower.CurrentUserId);
                    users.Remove(removedUser);
                }
                var currentUser = await _unitOfWork.UserRepository.GetUserById(int.Parse(userId));
                users.Remove(currentUser);

                var mappedUsers = Mapper.Map<ICollection<User>, ICollection<UserDTO>>(users);

                if (number < mappedUsers.Count())
                    return Ok(mappedUsers.Skip(number).Take(number + 15));
                else return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchPeopleByUsername"),Authorize]
        public async Task<IActionResult> SearchPeopleByUsername(string username)
        {
            try
            {
                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var currentUser = await _unitOfWork.UserRepository.GetUserById(int.Parse(userId));

                var users = _unitOfWork.UserRepository.GetPeopleByUsername(username);
                if(!users.Result.IsNullOrEmpty())
                {
                    users.Result.Remove(currentUser);
                    var mappedUsers = Mapper.Map<ICollection<User>, ICollection<UserDTO>>(users.Result);

                    return Ok(mappedUsers);
                }

                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFollowed"), Authorize]
        public async Task<IActionResult> GetFollowed(int number)
        {
            try {

                var bearer_token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(bearer_token) as JwtSecurityToken;

                var userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

                var followed = await _unitOfWork.UserFollowerRepository.GetByFollowerAsync(int.Parse(userId));
                var users = new List<User>();
                foreach(var f in followed)
                {
                    users.Add(await _unitOfWork.UserRepository.GetUserById((int)f.CurrentUserId));
                }

                var mappedUsers = Mapper.Map<ICollection<User>, ICollection<UserDTO>>(users);

                if (number < mappedUsers.Count())
                    return Ok(mappedUsers.Skip(number).Take(number + 15));
                else return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
