using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesAPI.Models
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long? Id { get; set; }

        [Required]
        public long? Version { get; set; }

        public string Name { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }
    }
}
