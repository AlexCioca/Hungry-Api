using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpGet("GetAllCategories"), Authorize]
        public async Task<IActionResult> GetAllRecipes()
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetAllAsync();
                if(category == null)
                {
                    return NotFound();
                }
                var mappedCategory = Mapper.Map<ICollection<Category>, ICollection<CategoryDTO>>(category);
                return Ok(mappedCategory);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
