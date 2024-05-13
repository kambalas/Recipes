using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace RecipesAPI.Models
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long? Id { get; set; }

        [Required]
        public long? Version { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        [Required]
        public DateTime? UpdatedAt { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public string? Description { get; set; }

        public long? PreparationTimeInSeconds { get; set; }

        public long? CookingTimeInSeconds { get; set; }

        public long? Servings { get; set; }

        public long? EnergyInKCal { get; set; }

        public ComplexityLevel? Level { get; set; }

        public IEnumerable<Step> Steps { get; set; } = Enumerable.Empty<Step>();

    }
}
