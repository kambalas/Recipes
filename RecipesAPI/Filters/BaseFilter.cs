using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PoS.Application.Filters
{
    /// <summary>
    /// Base class for filter criteria.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// The page number for pagination. Defaults to 1. Example: 1
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The size of each page for pagination. Must be between 1 and the maximum page size. Example: 10
        /// </summary>
        [Range(1, DefaultPaginationParameters.MaximumPageSize)]
        public int PageSize { get; set; } = DefaultPaginationParameters.MaximumPageSize;

        /// <summary>
        /// The property of database entity model to order the results by. Example: "Name"
        /// </summary>
        public string OrderBy { get; set; } = string.Empty;

        /// <summary>
        /// The sorting direction. Can be "asc" for ascending or "dsc" for descending. Example: "asc"
        /// </summary>
        public Sorting? Sorting { get; set; } = null;

        /// <summary>
        /// Calculates the number of items to skip based on the current page and page size.
        /// </summary>
        /// <returns>The number of items to skip.</returns>
        public int ItemsToSkip()
        {
            return (Page - 1) * PageSize;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        asc,
        dsc
    }
}
