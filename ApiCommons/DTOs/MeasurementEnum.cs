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
        /// Enum KgEnum for kg
        /// </summary>
        [EnumMember(Value = "kg")]
        KgEnum = 0,
        /// <summary>
        /// Enum GEnum for g
        /// </summary>
        [EnumMember(Value = "g")]
        GEnum = 1,
        /// <summary>
        /// Enum LEnum for l
        /// </summary>
        [EnumMember(Value = "l")]
        LEnum = 2,
        /// <summary>
        /// Enum MlEnum for ml
        /// </summary>
        [EnumMember(Value = "ml")]
        MlEnum = 3,
        /// <summary>
        /// Enum TspEnum for tsp
        /// </summary>
        [EnumMember(Value = "tsp")]
        TspEnum = 4,
        /// <summary>
        /// Enum TbspEnum for tbsp
        /// </summary>
        [EnumMember(Value = "tbsp")]
        TbspEnum = 5,
        /// <summary>
        /// Enum PieceEnum for piece
        /// </summary>
        [EnumMember(Value = "piece")]
        PieceEnum = 6
    }
    
    
}
