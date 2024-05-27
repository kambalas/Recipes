using RecipesAPI.Models;
using Ingredient = RecipesAPI.Models.Ingredient;
using ApiCommons.DTOs;
using static ApiCommons.DTOs.LevelEnum;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using System.Net.Http;
using System.Security.Claims;

namespace RecipesAPI.Mappers
{
    public class Mappers : IMappers
    {
        public Recipe ToRecipe(RecipeRequest recipeRequest, long userId)
        {
            
            var defaultDateTime = DateTime.UtcNow;
			return new Recipe()
            {
                Version = recipeRequest.Version ?? new byte[] {},
                Name = recipeRequest.Name ?? "default",
                ImageURL = recipeRequest.ImageEncoded,
                CreatedAt = defaultDateTime,
				UpdatedAt = defaultDateTime,
                RecipeIngredients = recipeRequest.Ingredients != null ? recipeRequest.Ingredients.ToList().Select(ingrDTO => 
                    new RecipeIngredient() {
                        Amount = (int)(ingrDTO.Amount ?? 0),
                        IngredientId = ingrDTO.Id,
                        MeasurementType = ToMeasurementType(ingrDTO.Measurement)
                    }).ToList() : new List<RecipeIngredient>(),
                Description = recipeRequest.Description,
                PreparationTimeInSeconds = recipeRequest.CookingDuration,
                CookingTimeInSeconds = recipeRequest.CookingDuration,
                Servings = recipeRequest.Servings,
                EnergyInKCal = recipeRequest.Energy,
                Level = ToLevel((LevelEnum)recipeRequest.Level),
                Steps = recipeRequest.Steps.Select(stepDto => ToStep(stepDto)).ToList(),
                UserId = userId
            };
            
            throw new NotImplementedException();
        }



        private LevelEnum ToDTOLevel(ComplexityLevel level)
        {
            switch (level)
            {
                case ComplexityLevel.Easy:
                    {
                        return Easy;
                    }
                case ComplexityLevel.Medium:
                    {
                        return Medium;
                    }
                case ComplexityLevel.Hard:
                    {
                        return Hard;
                    }
                default: break;
            }

            throw new NotImplementedException();
        }

        private ComplexityLevel ToLevel(LevelEnum levelEnum)
        {
            switch (levelEnum)
            {
                case Easy:
                    return ComplexityLevel.Easy;
                case Medium:
                    return ComplexityLevel.Medium;
                case Hard:
                    return ComplexityLevel.Hard;
                default:
                    throw new NotImplementedException();
            }
        }


        public RecipeResponse ToRecipeResponse(Recipe recipe)
        {
            
            var recipeDTO = new RecipeResponse
            {
                Id = recipe.Id,
                Version = recipe.Version,
                Name = recipe.Name ?? "default",
                Description = recipe.Description,
                ImageURL = recipe.ImageURL,
                Ingredients = recipe.RecipeIngredients != null ? recipe.RecipeIngredients.Select(ri => ToIngredientResponse(ri)).ToList() : new List<RecipeIngredientResponse>(),
                Steps = recipe.Steps != null ? recipe.Steps.Select(step => ToStepResponse(step)).ToList() : new List<StepResponse>(),
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                Servings = recipe.Servings,
                CookingDuration = recipe.CookingTimeInSeconds,
                PreparationDuration = recipe.PreparationTimeInSeconds,
                Energy = recipe.EnergyInKCal,
                Level = 0,
                UserId = recipe.User.Id

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
                Ingredients = recipe.RecipeIngredients != null ? recipe.RecipeIngredients.Select(ri => ToIngredientResponse(ri)).ToList() : new List<RecipeIngredientResponse>(),
                Steps = recipe.Steps != null ? recipe.Steps.Select(step => ToStepResponse(step)).ToList() : new List<StepResponse>(),
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
                Id = recipeIngredient.IngredientId,
                Name = recipeIngredient.Ingredient.Name,
                Measurement = ToMeasurement(recipeIngredient.MeasurementType),
                Amount = recipeIngredient.Amount,
            };
            return recipeIngredientDTO;
        }

        public IngredientResponse ToIngredientResponse(Ingredient ingredient)
        {
            var ingredientDTO = new IngredientResponse
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                //Measurement = ToMeasurement(ingredient.MeasurementType),
                //Amount = 0, 
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
                        return MeasurementEnum.g;
                    }
                case MeasurementType.Kilogram:
                    {
                        return MeasurementEnum.kg;
                    }
                case MeasurementType.MiliLitre:
                    {
                        return MeasurementEnum.ml;
                    }
                case MeasurementType.Litre:
                    {
                        return MeasurementEnum.l;
                    }
                case MeasurementType.Piece:
                    {
                        return MeasurementEnum.piece;
                    }
                case MeasurementType.Tablespoon:
                    {
                        return MeasurementEnum.tbsp;
                    }

                case MeasurementType.Teaspoon:
                    {
                        return MeasurementEnum.tsp;
                    }
                default: break;
            }

            throw new NotImplementedException();
        }

        private MeasurementType ToMeasurementType(MeasurementEnum measurementEnum)
        {
            switch (measurementEnum)
            {
                case MeasurementEnum.g:
                    {
                        return MeasurementType.Gram;
                    }
                case MeasurementEnum.kg:
                    {
                        return MeasurementType.Kilogram;
                    }
                case MeasurementEnum.ml:
                    {
                        return MeasurementType.MiliLitre;
                    }
                case MeasurementEnum.l:
                    {
                        return MeasurementType.Litre;
                    }
                case MeasurementEnum.piece:
                    {
                        return MeasurementType.Piece;
                    }
                case MeasurementEnum.tbsp:
                    {
                        return MeasurementType.Tablespoon;
                    }
                case MeasurementEnum.tsp:
                    {
                        return MeasurementType.Teaspoon;
                    }
                default:
                    break;
            }

            throw new NotImplementedException();
        }


        public Ingredient ToIngredient(IngredientRequest ingredientRequest)
        {
            return new Ingredient()
            {
				Name = ingredientRequest.Name,     
                //MeasurementType = MeasurementType.Gram
			};
        }

        public Step ToStep(StepRequest stepRequest)
        {
            return new Step()
            {
                //Id = stepDTO.Id is null ? 0L : (long)stepDTO.Id, Should be autogenerated
                Id = null,
                Description = stepRequest.Description,
                Phase = stepRequest.Phase is null ? 0L : ToPhase((PhaseEnum)stepRequest.Phase),
                Index = stepRequest.StepNumber is null ? 0 : (int)stepRequest.StepNumber,
            };
        }

        private StepPhase ToPhase(PhaseEnum phase)
        {
            switch (phase)
            {
                case PhaseEnum.Prep:
                    {
                        return StepPhase.Preparation;
                    }
                case PhaseEnum.Cooking:
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
                        return PhaseEnum.Prep;
                    }
                case StepPhase.Cooking:
                    {
                        return PhaseEnum.Cooking;
                    }
            }

            throw new NotImplementedException();
        }

        public User ToUser(UserRequest userRequest)
        {
            return new User()
            {
                Id = null,
                Version = new byte[] {},
                Username = userRequest.Username,
                Email = userRequest.Email,
                Password = userRequest.Password,
            };
        }

        public UserResponse ToUserResponse(User user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Version = user.Version,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
            };
        }

        public LogInResponse ToLogInResponse(string token)
        {
            return new LogInResponse()
            {
                JWTToken = token
            };
        }
    }
}