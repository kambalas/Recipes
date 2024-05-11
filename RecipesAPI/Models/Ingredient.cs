using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesAPI.Models
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long Version { get; set; }

        public string Name { get; set; }

        public MeasurementType MeasurementType { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }
    }
}
