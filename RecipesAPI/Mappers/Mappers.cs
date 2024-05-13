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
            /*return new Recipe()
            {
                Id = recipeDTO.Id is null ? 0L : (long)recipeDTO.Id,
                Version = recipeDTO.Version is null ? 0L : (long)recipeDTO.Version,
                Name = recipeDTO.Name,
                CreatedAt = recipeDTO.CreatedAt is null ? DateTime.UtcNow : (DateTime)recipeDTO.CreatedAt,
                UpdatedAt = recipeDTO.UpdatedAt is null ? DateTime.UtcNow : (DateTime)recipeDTO.UpdatedAt,
                Ingredients = recipeDTO.Ingredients.Select(ingrDTO => ToIngredient(ingrDTO)).ToList(),
                Description = recipeDTO.Description,
                PreparationTimeInSeconds = 0, // TODO: FIX THIS
                CookingTimeInSeconds = recipeDTO.Duration, // TODO: FIX THIS
                Servings = recipeDTO.Servings,
                EnergyInKCal = recipeDTO.Energy,
                Level = recipeDTO.Id is null ? null : ToLevel((LevelEnum)recipeDTO.Level!),
                Steps = recipeDTO.Steps.Select(stepDto => ToStep(stepDto)).ToList(),
            };*/

            // Started doing create,
            // but did not finish, feel free to use,
            // this might not be correct though
            
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
                Name = recipe.Name,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients.Select(ingr => ToIngredientResponse(ingr)).ToList(),
                //Steps = recipe.Steps.Select(step => ToStepDTO(step)).ToList(),
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                Servings = recipe.Servings,
                Duration = recipe.CookingTimeInSeconds,
                Energy = recipe.EnergyInKCal,
                //Level = recipe.Level is null ? null : ToLevelDTO((ComplexityLevel)recipe.Level),
            };

            return recipeDTO;
        }

        public IngredientResponse ToIngredientResponse(Ingredient ingredient)
        {
            var ingredientDTO = new IngredientResponse
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Measurement = ToMeasurement(ingredient.MeasurementType),
                Amount = 0, // TODO: IMPLEMENT THIS
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
                Id = 0,
                //Id = ingredientDTO.Id is null ? 0L : (long)ingredientDTO.Id, Should be autogenerated
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