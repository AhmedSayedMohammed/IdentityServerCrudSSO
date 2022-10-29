using RecruitmentApp.Shared.Filter;

namespace RecruitmentApp.Core.Vacancy.Query.Models
{
    public class GetAllVacancysParameter : RequestParameter
    {
        public string SearchingWord { get; set; } = "";
    }
}
