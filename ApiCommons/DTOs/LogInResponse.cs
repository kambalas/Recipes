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
    public partial class LogInResponse : IEquatable<LogInResponse>
    {

        /// <summary>
        /// Gets or Sets JWTToken
        /// </summary>
        [Required]
        [DataMember(Name = "jwtToken")]
        public string JWTToken { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserRequest {\n");
            sb.Append("  JWTToken: ").Append(JWTToken).Append("\n");
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
            return obj.GetType() == GetType() && Equals((IngredientRequest)obj);
        }

        /// <summary>
        /// Returns true if Ingredient instances are equal
        /// </summary>
        /// <param name="other">Instance of Ingredient to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LogInResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    JWTToken == other.JWTToken ||
                    JWTToken != null &&
                    JWTToken.Equals(other.JWTToken)
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
                if (JWTToken != null)
                    hashCode = hashCode * 59 + JWTToken.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(LogInResponse left, LogInResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LogInResponse left, LogInResponse right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
