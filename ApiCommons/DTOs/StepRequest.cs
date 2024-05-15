using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiCommons.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class StepRequest : IEquatable<StepRequest>
    {
        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [Required]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Phase
        /// </summary>
        [Required]
        [DataMember(Name = "phase")]
        public PhaseEnum? Phase { get; set; }

        /// <summary>
        /// Gets or Sets StepNumber
        /// </summary>
        [Required]
        [DataMember(Name = "step_number")]
        public int? StepNumber { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StepRequest {\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Phase: ").Append(Phase).Append("\n");
            sb.Append("  StepNumber: ").Append(StepNumber).Append("\n");
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
            return obj.GetType() == GetType() && Equals((StepRequest)obj);
        }

        /// <summary>
        /// Returns true if Step instances are equal
        /// </summary>
        /// <param name="other">Instance of Step to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StepRequest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) &&
                (
                    Phase == other.Phase ||
                    Phase != null &&
                    Phase.Equals(other.Phase)
                ) &&
                (
                    StepNumber == other.StepNumber ||
                    StepNumber != null &&
                    StepNumber.Equals(other.StepNumber)
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
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (Phase != null)
                    hashCode = hashCode * 59 + Phase.GetHashCode();
                if (StepNumber != null)
                    hashCode = hashCode * 59 + StepNumber.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(StepRequest left, StepRequest right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StepRequest left, StepRequest right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
