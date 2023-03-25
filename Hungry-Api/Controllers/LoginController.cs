using AutoMapper;
using Hungry_Api.AuthFolder;
using Hungry_Api.AuthModels;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Hungry_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hungry_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:ControllerBase
    {
       
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        private AuthService _authService { get; }
        public LoginController( IMapper mapper, IUnitOfWork unitOfWork, AuthService authService)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._authService=authService;
        }
        
        [HttpPost("LoginWithGoogle")]
        public async Task<ActionResult> LoginWithGoogle([FromBody] UserLogin userLogin)
        {

            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = _authService.GenerateToken(user);

                user.Token = token;
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                return Ok(token);
            }

            return NotFound("Something bad happened");
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] InternalUserSignUp userLogin)
        {
            var user = _unitOfWork.UserRepository.GetAllAsync().Result.FirstOrDefault(x => x.Email == userLogin.Email && x.AccountId!=null);

            if (user == null) {


                User currentUser = new User();
                currentUser.Email = userLogin.Email;
                currentUser.Role = "USER";
                currentUser.Username = userLogin.Username;
                currentUser.LastName = userLogin.LastName;
                currentUser.FirstName = userLogin.FirstName;
                currentUser.Password= userLogin.Password;
                currentUser.Role = "USER";
                currentUser.AccountId = "";
                currentUser.Token = _authService.GenerateToken(currentUser);
                await _unitOfWork.UserRepository.AddAsync(currentUser);
                await _unitOfWork.CompleteAsync();

                return Ok(currentUser);
            }
            return BadRequest("User exists");
        }

        [HttpPost("InternalLogin")]
        public async Task<IActionResult> InternalLogin([FromBody] InternalUserLogin userLogin)
        {
            var user = _unitOfWork.UserRepository.GetAllAsync().Result.FirstOrDefault(x => x.Email == userLogin.Email  && x.Password==userLogin.Password && x.AccountId=="");
            
            if(user != null)
            {

                user.Token = _authService.GenerateToken(user);
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                return Ok(user.Token);

            }
            return BadRequest("Bad creditentials");

        }

          private User Authenticate(UserLogin userLogin)
        {
            
            var currentUser =  _unitOfWork.UserRepository.GetAllAsync().Result.FirstOrDefault(x => x.AccountId == userLogin.AccountId);


            if (currentUser != null)
            {
                return currentUser;
            }
            else
            {
                currentUser = new User();
                currentUser.Email = userLogin.Email;
                currentUser.AccountId = userLogin.AccountId;
                currentUser.Username=userLogin.Username;
                currentUser.LastName=userLogin.LastName;
                currentUser.FirstName=userLogin.FirstName;
                currentUser.Role = "USER";
                currentUser.Token = "";
                _unitOfWork.UserRepository.AddAsync(currentUser);
                _unitOfWork.CompleteAsync();
                return currentUser;
            }
           
        }


    }
}
