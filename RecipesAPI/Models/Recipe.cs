using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace RecipesAPI.Models
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long Version { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public string Description { get; set; }

        public int PreparationTimeInSeconds { get; set; }

        public int CookingTimeInSeconds { get; set; }

        public int Servings { get; set; }

        public int EnergyInKCal { get; set; }

        public ComplexityLevel Level { get; set; }

        public IEnumerable<Step> Steps { get; set; } = Enumerable.Empty<Step>();

    }
}
