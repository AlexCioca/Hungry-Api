using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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
                var user=_unitOfWork.UserRepository.GetUserById(id);
                if(user == null)
                {
                    return NotFound();
                }
                var mappedUser = Mapper.Map<User, UserDTO>(user.Result);

                return Ok(mappedUser);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
