using Google.Protobuf.WellKnownTypes;
using System.Runtime.Serialization;

namespace RecipesAPI.Models
{
    public enum MeasurementType
    {
        Kilogram = 0,
        Gram = 1,
        Litre = 2,
        MiliLitre = 3,
        Teaspoon = 4,
        Tablespoon = 5,
        Piece = 6
    }
}
