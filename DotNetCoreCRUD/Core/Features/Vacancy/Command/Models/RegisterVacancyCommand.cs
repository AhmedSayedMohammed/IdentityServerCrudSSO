using MediatR;
using RecruitmentApp.Shared.Wrapper;

namespace RecruitmentApp.Core.Vacancy.Command.Models
{
    public class RegisterVacancyCommand : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? Responsibilities { get; set; }
        public string? Skills { get; set; }
        public string? JobCategory { get; set; }

    }
}
