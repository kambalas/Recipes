using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiCommons.DTOs
{
    /// <summary>
    /// Gets or Sets Measurement
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MeasurementEnum
    {
        /// <summary>
        /// Enum kg for kg
        /// </summary>
        [EnumMember(Value = "kg")]
        kg = 0,
        /// <summary>
        /// Enum g for g
        /// </summary>
        [EnumMember(Value = "g")]
        g = 1,
        /// <summary>
        /// Enum l for l
        /// </summary>
        [EnumMember(Value = "l")]
        l = 2,
        /// <summary>
        /// Enum ml for ml
        /// </summary>
        [EnumMember(Value = "ml")]
        ml = 3,
        /// <summary>
        /// Enum tsp for tsp
        /// </summary>
        [EnumMember(Value = "tsp")]
        tsp = 4,
        /// <summary>
        /// Enum tbsp for tbsp
        /// </summary>
        [EnumMember(Value = "tbsp")]
        tbsp = 5,
        /// <summary>
        /// Enum piece for piece
        /// </summary>
        [EnumMember(Value = "piece")]
        piece = 6
    }
    
    
}
