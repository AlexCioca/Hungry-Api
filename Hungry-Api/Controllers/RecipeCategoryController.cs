using AutoMapper;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeCategoryController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public RecipeCategoryController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }
    }
}
