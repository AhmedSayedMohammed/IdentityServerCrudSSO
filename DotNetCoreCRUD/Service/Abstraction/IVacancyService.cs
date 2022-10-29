using RecruitmentApp.Models;

namespace RecruitmentApp.Service.Abstraction
{
    public interface IVacancyService
    {
        Task<Vacancy> GetByIdAsync(int Id);
    }
}
