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
        EasyEnum = 0,
        /// <summary>
        /// Enum MediumEnum for medium
        /// </summary>
        [EnumMember(Value = "medium")]
        MediumEnum = 1,
        /// <summary>
        /// Enum HardEnum for hard
        /// </summary>
        [EnumMember(Value = "hard")]
        HardEnum = 2
    }
}
