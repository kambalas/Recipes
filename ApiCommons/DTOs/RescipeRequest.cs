using IO.Swagger.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static ApiCommons.DTOs.LevelEnum;

namespace ApiCommons.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class RecipeRequest : IEquatable<RecipeRequest>
    {
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [Required]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets ImageURL
        /// </summary>
        [DataMember(Name = "imageUrl")]
        public string? ImageEncoded { get; set; }

        /// <summary>
        /// Gets or Sets Ingredients
        /// </summary>
        [Required]
        [DataMember(Name = "ingredients")]
        public List<RecipeIngredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or Sets Steps
        /// </summary>
        [Required]
        [DataMember(Name = "steps")]
        public List<StepRequest> Steps { get; set; }

        /// <summary>
        /// Gets or Sets Servings
        /// </summary>
        [DataMember(Name = "servings")]
        public long? Servings { get; set; }

        /// <summary>
        /// Gets or Sets CookingDuration
        /// </summary>
        [DataMember(Name = "cookingDuration")]
        public long? CookingDuration { get; set; }

        /// <summary>
        /// Gets or Sets PreparationDuration
        /// </summary>
        [DataMember(Name = "preparationDuration")]
        public long? PreparationDuration { get; set; }

        /// <summary>
        /// Gets or Sets Energy
        /// </summary>
        [DataMember(Name = "energy")]
        public long? Energy { get; set; }

        /// <summary>
        /// Gets or Sets Level
        /// </summary>
        [DataMember(Name = "level")]
        public LevelEnum? Level { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecipeRequest {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Ingredients: ").Append(Ingredients).Append("\n");
            sb.Append("  Steps: ").Append(Steps).Append("\n");
            sb.Append("  Servings: ").Append(Servings).Append("\n");
            sb.Append("  CookingDuration: ").Append(CookingDuration).Append("\n");
            sb.Append("  PreparationDuration: ").Append(PreparationDuration).Append("\n");
            sb.Append("  Energy: ").Append(Energy).Append("\n");
            sb.Append("  Level: ").Append(Level).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((RecipeRequest)obj);
        }

        /// <summary>
        /// Returns true if Recipe instances are equal
        /// </summary>
        /// <param name="other">Instance of Recipe to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecipeRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) &&
                (
                    Ingredients == other.Ingredients ||
                    Ingredients != null &&
                    Ingredients.SequenceEqual(other.Ingredients)
                ) &&
                (
                    Steps == other.Steps ||
                    Steps != null &&
                    Steps.SequenceEqual(other.Steps)
                ) &&
                (
                    Servings == other.Servings ||
                    Servings != null &&
                    Servings.Equals(other.Servings)
                ) &&
                (
                    CookingDuration == other.CookingDuration ||
                    CookingDuration != null &&
                    CookingDuration.Equals(other.CookingDuration)
                ) &&
                (
                    PreparationDuration == other.PreparationDuration ||
                    PreparationDuration != null &&
                    PreparationDuration.Equals(other.PreparationDuration)
                ) &&
                (
                    Energy == other.Energy ||
                    Energy != null &&
                    Energy.Equals(other.Energy)
                ) &&
                (
                    Level == other.Level ||
                    Level != null &&
                    Level.Equals(other.Level)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (Ingredients != null)
                    hashCode = hashCode * 59 + Ingredients.GetHashCode();
                if (Steps != null)
                    hashCode = hashCode * 59 + Steps.GetHashCode();
                if (Servings != null)
                    hashCode = hashCode * 59 + Servings.GetHashCode();
                if (CookingDuration != null)
                    hashCode = hashCode * 59 + CookingDuration.GetHashCode();
                if (PreparationDuration != null)
                    hashCode = hashCode * 59 + PreparationDuration.GetHashCode();
                if (Energy != null)
                    hashCode = hashCode * 59 + Energy.GetHashCode();
                if (Level != null)
                    hashCode = hashCode * 59 + Level.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class RecipeIngredient
        {
            [Required]

            [DataMember(Name = "id")]
            public long? Id { get; set; }
            /// <summary>
            /// Gets or Sets Measurement
            /// </summary>
            [Required]
            [DataMember(Name = "measurement")]
            public MeasurementEnum? Measurement { get; set; }

            /// <summary>
            /// Gets or Sets Amount
            /// </summary>
            [Required]
            [DataMember(Name = "amount")]
            public long? Amount { get; set; }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(RecipeRequest left, RecipeRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RecipeRequest left, RecipeRequest right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
