namespace RecruitmentApp.Core.Vacancy.Query.Models
{
    public class GetAllVacancyResponse
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Responsibilities { get; set; }
        public string? Skills { get; set; }
        public string? JobCategory { get; set; }
    }
}
