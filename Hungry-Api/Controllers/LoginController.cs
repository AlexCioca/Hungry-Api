using AutoMapper;
using Hungry_Api.AuthModels;
using Hungry_Api.DbModels;
using Hungry_Api.Repository.Interface;
using Hungry_Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Hungry_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        private PasswordHasher _passwordHasher;
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
            var user = _unitOfWork.UserRepository.GetAllAsync().Result.FirstOrDefault(x =>( x.Email == userLogin.Email && x.AccountId!=null) ||( x.Username == userLogin.Username && x.AccountId != null));

            if (user == null) {

                
                User currentUser = new User();
                currentUser.Email = userLogin.Email;
                currentUser.Role = "USER";
                currentUser.Username = userLogin.Username;
                currentUser.LastName = userLogin.LastName;
                currentUser.FirstName = userLogin.FirstName;
                currentUser.Password= PasswordHasher.HashPassword(userLogin.Password); 
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

         
            var user = _unitOfWork.UserRepository.GetAllAsync().Result.SingleOrDefault(x => x.Email == userLogin.Email);

            var decr = PasswordHasher.verifyPassword(userLogin.Password, user.Password);
            if (decr)
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
            
            var currentUser =  _unitOfWork.UserRepository.GetAllAsync().Result.FirstOrDefault(x => x.Email == userLogin.Email);
           
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
