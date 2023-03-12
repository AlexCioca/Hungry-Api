using AutoMapper;
using Hungry_Api.DbModels;
using Hungry_Api.DTO;
using Hungry_Api.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Hungry_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController:ControllerBase
    {
        private IMapper Mapper { get; }
        private readonly IUnitOfWork _unitOfWork;
        public IngredientController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.Mapper = mapper;
            this._unitOfWork = unitOfWork;

        }

        [HttpGet("GetIngredientsForARecipe")]
        public async Task<IActionResult> GetIngredientsForARecipe(int recipeId)
        {
            try
            {
                var ingredients=_unitOfWork.IngredientRepository.GetIngredientsForRecipe(recipeId);
                var mapped = Mapper.Map<Ingredient[], IngredientDTO[]>(ingredients.Result.ToArray());
                return Ok(mapped);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
