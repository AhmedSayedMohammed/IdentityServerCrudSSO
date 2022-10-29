using MediatR;
using RecruitmentApp.Shared.Wrapper;

namespace RecruitmentApp.Core.Vacancy.Query.Models
{
    public class GetAllVacancysQuery : IRequest<PagedResponse<IEnumerable<GetAllVacancyResponse>>>
    {
        #region Vars / Props

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        #endregion
        public GetAllVacancysQuery(int pageNumber = 1, int pageSize = 10, string searchString = "")
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }
}
