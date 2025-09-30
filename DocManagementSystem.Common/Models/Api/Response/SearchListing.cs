using System.Collections;
using DocManagementSystem.Common.Models.Api.Request;
namespace DocManagementSystem.Common.Models.Api.Response
{
    public class SearchListing
    {
        public Object Records { get; set; } = new Object();
        public long TotalResults { get; set; }
        public int? PageNo { get; set; }
        public bool? IsListView { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPages { get; set; }
        public int? NextPageNo { get; set; }
        public int? PrevPageNo { get; set; }
        public bool? HasPrevPage { get; set; }
        public bool? HasNextPage { get; set; }
        public List<Filter>? SearchQuery { get; set; }
        public bool SkipPagination { get; set; }
        public string? ContentType { get; set; }
        public string? CurrentItemId { get; set; }
        public List<Filter>? Filters { get; set; }
        public SearchListing(Object records, long totalResults, SearchBody request, bool? IsListView = false)
        {
            this.Records = records;

            this.TotalResults = totalResults;
            this.PageNo = request.PageNo;
            this.PageSize = request.PageSize;
            this.SearchQuery = request.SearchQuery;
            this.SkipPagination = request.SkipPagination;
            this.ContentType = request.ContentType;
            this.Filters = request.Filters;
            this.IsListView = request.IsListView;

            if (PageSize.HasValue)
            {
                var aaaa = (decimal)this.TotalResults / (decimal)this.PageSize.Value;
                this.TotalPages = ((int)Math.Round(aaaa, MidpointRounding.ToPositiveInfinity));
            }
            if (this.PageNo > 1) this.PrevPageNo = PageNo - 1;
            if (this.PageNo < this.TotalPages) this.NextPageNo = PageNo + 1;
            this.HasPrevPage = this.PrevPageNo.HasValue;
            this.HasNextPage = this.NextPageNo.HasValue;

            //If after filter, number of records are less than page-size that means there are no more records for the specified filter so change the following values
            if ((records is System.Collections.IList) && (records as IList).Count < PageSize)
            {
                this.NextPageNo = PageNo;
                this.HasNextPage = false;
            }

        }
    }
}
