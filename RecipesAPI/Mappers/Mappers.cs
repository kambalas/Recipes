using IO.Swagger.Models;
using RecipesAPI.Models;
using Ingredient = RecipesAPI.Models.Ingredient;
using static IO.Swagger.Models.StepResponse;
using ApiCommons.DTOs;
using static ApiCommons.DTOs.LevelEnum;
using Microsoft.AspNetCore.Http;

namespace RecipesAPI.Mappers
{
    public class Mappers : IMappers
    {
        public Recipe ToRecipe(RecipeRequest recipeRequest)
        {

			var defaultDateTime = DateTime.UtcNow;
			return new Recipe()
            {
                Version = 1,
                Name = recipeRequest.Name ?? "default",
                ImageURL = "https://google.com",
                CreatedAt = defaultDateTime,
				UpdatedAt = defaultDateTime,
                Ingredients = recipeRequest.Ingredients.ToList().Select(ingrDTO => ToIngredient(ingrDTO)).ToList(),
                Description = recipeRequest.Description,
                PreparationTimeInSeconds = recipeRequest.CookingDuration,
                CookingTimeInSeconds = recipeRequest.CookingDuration,
                Servings = recipeRequest.Servings,
                EnergyInKCal = recipeRequest.Energy,
                Level = 0,
                Steps = recipeRequest.Steps.Select(stepDto => ToStep(stepDto)).ToList(),
            };
            
            throw new NotImplementedException();
        }



        private LevelEnum ToLevel(ComplexityLevel level)
        {
            switch (level)
            {
                case ComplexityLevel.Easy:
                    {
                        return EasyEnum;
                    }
                case ComplexityLevel.Medium:
                    {
                        return MediumEnum;
                    }
                case ComplexityLevel.HardEnum:
                    {
                        return HardEnum;
                    }
                default: break;
            }

            throw new NotImplementedException();
        }

        public RecipeResponse ToRecipeResponse(Recipe recipe)
        {
            
            var recipeDTO = new RecipeResponse
            {
                Id = recipe.Id,
                Version = recipe.Version,
                UserId = null,
                Name = recipe.Name ?? "default",
                Description = recipe.Description,
                ImageURL = recipe.ImageURL,
                Ingredients = recipe.RecipeIngredients?.Select(ri => ToIngredientResponse(ri)).ToList(),
                Steps = recipe.Steps.Select(step => ToStepResponse(step)).ToList(),
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                Servings = recipe.Servings,
                CookingDuration = recipe.CookingTimeInSeconds,
                PreparationDuration = recipe.PreparationTimeInSeconds,
                Energy = recipe.EnergyInKCal,
                Level = 0,
            };

            return recipeDTO;
        }

        public RecipeResponse ToRecipeResponseOnCreate(Recipe recipe)
        {

            var recipeDTO = new RecipeResponse
            {
                Id = recipe.Id,
                Version = recipe.Version,
                UserId = null,
                Name = recipe.Name ?? "default",
                Description = recipe.Description,
                ImageURL = recipe.ImageURL,
                Ingredients = recipe.Ingredients?.Select(ri => ToIngredientResponse(ri)).ToList(),
                Steps = recipe.Steps.Select(step => ToStepResponse(step)).ToList(),
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                Servings = recipe.Servings,
                CookingDuration = recipe.CookingTimeInSeconds,
                PreparationDuration = recipe.PreparationTimeInSeconds,
                Energy = recipe.EnergyInKCal,
                Level = 0,
            };

            return recipeDTO;
        }

        public RecipeIngredientResponse ToIngredientResponse(RecipeIngredient recipeIngredient)
        {
            var recipeIngredientDTO = new RecipeIngredientResponse
            {
                Id = recipeIngredient.Ingredient.Id,
                Name = recipeIngredient.Ingredient.Name,
                Measurement = ToMeasurement(recipeIngredient.MeasurementType),
                Amount = recipeIngredient.Amount,
            };
            return recipeIngredientDTO;
        }

        public RecipeIngredientResponse ToIngredientResponse(Ingredient ingredient)
        {
            var ingredientDTO = new RecipeIngredientResponse
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                //Measurement = ToMeasurement(ingredient.MeasurementType),
                Amount = 0, 
            };
            return ingredientDTO;
        }

        public StepResponse ToStepResponse(Step step)
        {
            return new StepResponse
            {
                Id = (int?)step.Id,
                Description = step.Description,
                Phase = ToPhaseResponse(step.Phase),
                StepNumber = step.Index,
            };
        }

        private MeasurementEnum ToMeasurement(MeasurementType measurementType)
        {
            switch (measurementType)
            {
                case MeasurementType.Gram:
                    {
                        return MeasurementEnum.GEnum;
                    }
                case MeasurementType.Kilogram:
                    {
                        return MeasurementEnum.KgEnum;
                    }
                case MeasurementType.MiliLitre:
                    {
                        return MeasurementEnum.MlEnum;
                    }
                case MeasurementType.Litre:
                    {
                        return MeasurementEnum.LEnum;
                    }
                case MeasurementType.Piece:
                    {
                        return MeasurementEnum.PieceEnum;
                    }
                default: break;
            }

            throw new NotImplementedException();
        }

        public Ingredient ToIngredient(IngredientRequest ingredientRequest)
        {
            return new Ingredient()
            {
                Version = 1,
				Name = ingredientRequest.Name,     
                //MeasurementType = MeasurementType.Gram
			};
        }

        public Step ToStep(StepRequest stepRequest)
        {
            return new Step()
            {
                //Id = stepDTO.Id is null ? 0L : (long)stepDTO.Id, Should be autogenerated
                Id = 0,
                Description = stepRequest.Description,
                Phase = stepRequest.Phase is null ? 0L : ToPhase((PhaseEnum)stepRequest.Phase),
                Index = stepRequest.StepNumber is null ? 0 : (int)stepRequest.StepNumber,
            };
        }

        private StepPhase ToPhase(PhaseEnum phase)
        {
            switch (phase)
            {
                case PhaseEnum.PrepEnum:
                    {
                        return StepPhase.Preparation;
                    }
                case PhaseEnum.CookingEnum:
                    {
                        return StepPhase.Cooking;
                    }
            }

            throw new NotImplementedException();
        }

        private PhaseEnum ToPhaseResponse(StepPhase phase)
        {
            switch (phase)
            {
                case StepPhase.Preparation:
                    {
                        return PhaseEnum.PrepEnum;
                    }
                case StepPhase.Cooking:
                    {
                        return PhaseEnum.CookingEnum;
                    }
            }

            throw new NotImplementedException();
        }
    }
}