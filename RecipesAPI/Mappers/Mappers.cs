using IO.Swagger.Models;
using RecipesAPI.Models;
using IngredientDTO = IO.Swagger.Models.Ingredient;
using Ingredient = RecipesAPI.Models.Ingredient;
using static IO.Swagger.Models.RecipeDTO;
using Step = RecipesAPI.Models.Step;
using StepDTO = IO.Swagger.Models.Step;
using static IO.Swagger.Models.Step;
using static IO.Swagger.Models.Ingredient;

namespace RecipesAPI.Mappers
{
    public class Mappers : IMappers
    {
        public Recipe ToRecipe(RecipeDTO recipeDTO)
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

        private ComplexityLevel ToLevel(LevelEnum levelDTO)
        {
            switch (levelDTO)
            {
                case LevelEnum.EasyEnum:
                    {
                        return ComplexityLevel.Easy;
                    }
                case LevelEnum.MediumEnum:
                    {
                        return ComplexityLevel.Medium;
                    }
                case LevelEnum.HardEnum:
                    {
                        return ComplexityLevel.HardEnum;
                    }
                default: break;
            }

            throw new NotImplementedException();
        }

        private LevelEnum ToLevelDTO(ComplexityLevel level)
        {
            switch (level)
            {
                case ComplexityLevel.Easy:
                    {
                        return LevelEnum.EasyEnum;
                    }
                case ComplexityLevel.Medium:
                    {
                        return LevelEnum.MediumEnum;
                    }
                case ComplexityLevel.HardEnum:
                    {
                        return LevelEnum.HardEnum;
                    }
                default: break;
            }

            throw new NotImplementedException();
        }

        public RecipeDTO ToRecipeDTO(Recipe recipe)
        {
            var recipeDTO = new RecipeDTO
            {
                Id = recipe.Id,
                Version = recipe.Version,
                UserId = null,
                Name = recipe.Name,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients.Select(ingr => ToIngredientDTO(ingr)).ToList(),
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

        private IngredientDTO ToIngredientDTO(Ingredient ingredient)
        {
            var ingredientDTO = new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Measurement = ToMeasurementDTO(ingredient.MeasurementType),
                Amount = 0, // TODO: IMPLEMENT THIS
            };
            return ingredientDTO;
        }

        private StepDTO ToStepDTO(Step step)
        {
            return new StepDTO
            {
                Id = (int?)step.Id,
                Description = step.Description,
                Phase = ToPhaseDTO(step.Phase),
                StepNumber = step.Index,
            };
        }

        private MeasurementEnum ToMeasurementDTO(MeasurementType measurementType)
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

        private Ingredient ToIngredient(IngredientDTO ingredientDTO)
        {
            return new Ingredient()
            {
                Id = ingredientDTO.Id is null ? 0L : (long)ingredientDTO.Id,
            };
        }

        private Step ToStep(StepDTO stepDTO)
        {
            return new Step()
            {
                Id = stepDTO.Id is null ? 0L : (long)stepDTO.Id,
                Description = stepDTO.Description,
                Phase = stepDTO.Phase is null ? 0L : ToPhase((PhaseEnum)stepDTO.Phase),
                Index = stepDTO.StepNumber is null ? 0 : (int)stepDTO.StepNumber,
            };
        }

        private StepPhase ToPhase(PhaseEnum phaseDTO)
        {
            switch (phaseDTO)
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

        private PhaseEnum ToPhaseDTO(StepPhase phase)
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