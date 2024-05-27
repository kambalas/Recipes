using ApiCommons.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Filters;
using RecipesAPI.Mappers;
using RecipesAPI.Services;
using RecipesAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace RecipesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientApiController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMappers _mappers;

        public IngredientApiController(IIngredientService recipeService, IMappers mappers)
        {
            _ingredientService = recipeService;
            _mappers = mappers;
        }

        /// <summary>
        /// List recipes
        /// </summary>
        /// <param name="filter">Filter criteria for ingredients</param>
        /// <response code="200">A list of recipes</response>
        [HttpGet]
        [Route("/v1/ingredients")]
        //[Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [SwaggerOperation("IngredientsGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<RecipeResponse>), description: "A list of ingredient")]
        public async Task<IActionResult> IngredientsGet([FromQuery] IngredientFilter filter)
        {
            var recipes = await _ingredientService.GetIngredientAsync(filter);
            var recipeDTOs = recipes.Select(recipe => _mappers.ToIngredientResponse(recipe));
            return Ok(recipeDTOs);
        }
    }
}
