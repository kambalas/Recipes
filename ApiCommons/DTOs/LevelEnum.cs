using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiCommons.DTOs
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LevelEnum
    {
        /// <summary>
        /// Enum EasyEnum for easy
        /// </summary>
        [EnumMember(Value = "easy")]
        Easy = 0,
        /// <summary>
        /// Enum MediumEnum for medium
        /// </summary>
        [EnumMember(Value = "medium")]
        Medium = 1,
        /// <summary>
        /// Enum HardEnum for hard
        /// </summary>
        [EnumMember(Value = "hard")]
        Hard = 2
    }
}
