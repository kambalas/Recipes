using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesAPI.Models
{
    public class Step
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long? Id { get; set; }

        public Recipe Recipe { get; set; }

        public string? Description { get; set; }

        public StepPhase Phase { get; set; }

        public int Index { get; set; }
    }
}
