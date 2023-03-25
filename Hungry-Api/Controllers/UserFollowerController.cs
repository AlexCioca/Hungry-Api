using AutoMapper;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFollowerController:ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public UserFollowerController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
