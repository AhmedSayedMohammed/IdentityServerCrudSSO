using RecruitmentApp.Shared.Base;

namespace RecruitmentApp.Models
{
    public class Vacancy : BaseEntity
    {
        public string Name { get; set; }
        public string Responsibilities { get; set; }
        public string Skills { get; set; }
        public string JobCategory { get; set; }
    }
}
