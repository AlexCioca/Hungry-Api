using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeReviewController : ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public RecipeReviewController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }


        [HttpGet("GetReviewForUser"), Authorize]
        public async Task<IActionResult> GetReviewForUser(int id)
        {
            try
            {
                var reviews = await _unitOfWork.RecipeReviewRepository.GetReviewsForRecipe(id);
                if(reviews.IsNullOrEmpty())
                {
                    return NotFound();
                }
                var mappedReviews = Mapper.Map<ICollection<RecipeReview>, ICollection<RecipeReviewDTO>>(reviews);
                return Ok(reviews);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
