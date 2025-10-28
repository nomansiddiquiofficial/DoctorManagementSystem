using System.Collections;
using DocManagementSystem.Common.Models.Api.Request;
namespace DocManagementSystem.Common.Models.Api.Response
{
    public class SearchListing<T>
    {
        public int TotalResults { get; set; }
        public List<T> Results { get; set; }

        public SearchListing(List<T> entities)
        {
            Results = entities;
            TotalResults = entities?.Count ?? 0;
        }
    }

    public class SearchListing
    {
        public int TotalResults { get; set; }
        public List<object> Results { get; set; }

        public SearchListing(List<object> entities)
        {
            Results = entities;
            TotalResults = entities?.Count ?? 0;
        }
    }

}
