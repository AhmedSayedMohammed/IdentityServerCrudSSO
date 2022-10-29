namespace RecruitmentApp.Shared.Wrapper
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public long? Count { get; set; }
        public int TotalPages { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, int totalPages = 0, long? count = 0)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Count = count;
            this.TotalPages = totalPages;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
