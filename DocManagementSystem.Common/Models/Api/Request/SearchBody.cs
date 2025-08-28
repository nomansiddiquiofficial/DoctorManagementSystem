
using System.Text.Json.Serialization;

namespace MajlDocManagementSystemis.Common.Models.Api.Request
{
    public class SearchBody
    {
        private int? pageSize;
        private int? pageNo;
        private List<Filter> searchQuery;
        private string? searchType;

        public List<Filter>? SearchQuery { get { return searchQuery ?? new List<Filter>(); } set { searchQuery = value; } }
        public bool IsListView { get; set; } = false;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public int? OmitId { get; set; }
        public bool IsKeywordSuggestion { get; set; }
        public int Skip => (PageSize.Value) * (PageNo.Value - 1);
        public int? PageNo { get { return pageNo ?? 1; } set { pageNo = value; } }
        public int? PageSize { get { return pageSize ?? 10; } set { pageSize = value; } }
        public bool? Featured { get; set; }
        public bool SkipPagination { get; set; }
        public string? ContentType { get; set; }
        public List<Filter>? Filters { get; set; }
    }
    public class Filter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }


}
