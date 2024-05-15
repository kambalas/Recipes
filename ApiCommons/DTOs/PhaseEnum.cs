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
        /// Enum PrepEnum for prep
        /// </summary>
        [EnumMember(Value = "prep")]
        PrepEnum = 0,
        /// <summary>
        /// Enum CookingEnum for cooking
        /// </summary>
        [EnumMember(Value = "cooking")]
        CookingEnum = 1
    }
}
