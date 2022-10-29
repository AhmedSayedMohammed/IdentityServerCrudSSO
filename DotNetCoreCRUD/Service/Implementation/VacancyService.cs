using FluentValidation;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
using RecruitmentApp.Service.Abstraction;

namespace RecruitmentApp.Service.Implementation
{
    public class VacancyService : IVacancyService
    {
        private readonly DataContext _context;

        public VacancyService(DataContext context)
        {
            _context = context;
        }


        public async Task<Vacancy> GetByIdAsync(int id)
        {
            var vac = _context.Vacancies.Find(id);
            await _context.SaveChangesAsync();
            if (vac == null)
                throw new ValidationException("Couldn't find this vacancy");
            return vac;

        }
    }
}
