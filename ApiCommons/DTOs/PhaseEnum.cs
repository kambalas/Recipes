using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiCommons.DTOs
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PhaseEnum
    {
        /// <summary>
        /// Enum Prep for prep
        /// </summary>
        [EnumMember(Value = "Prep")]
        Prep = 0,
        /// <summary>
        /// Enum Cooking for cooking
        /// </summary>
        [EnumMember(Value = "Cooking")]
        Cooking = 1
    }
}
