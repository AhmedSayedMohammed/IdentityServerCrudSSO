using AutoMapper;
using MediatR;
using RecruitmentApp.Core.Vacancy.Command.Models;
using RecruitmentApp.Core.Vacancy.Query.Models;
using RecruitmentApp.Data;
using RecruitmentApp.Shared.Wrapper;

namespace RecruitmentApp.Core.Vacancy.Handler
{
    public class VacancyHandler :
        IRequestHandler<RegisterVacancyCommand, Response<string>>,
        IRequestHandler<GetAllVacancysQuery, PagedResponse<IEnumerable<GetAllVacancyResponse>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VacancyHandler(DataContext context,

            IMapper mapper)
        {
            _mapper = mapper;

            _context = context;
        }


        public async Task<Response<string>> Handle(RegisterVacancyCommand request, CancellationToken cancellationToken)
        {
            Models.Vacancy vacancy = _mapper.Map<Models.Vacancy>(request);
            await _context.Vacancies.AddAsync(vacancy);
            return new Response<string>("Created Successfully");
        }

        public async Task<PagedResponse<IEnumerable<GetAllVacancyResponse>>> Handle(GetAllVacancysQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllVacancysParameter>(request);
            float totalCount = _context.Vacancies.Count();
            float totalPages = CalcPages(request.PageSize, totalCount);
            var nextPage = _context.Vacancies
                    .OrderBy(b => b.CreatedDate)
                    .Where(b => b.Name.Contains(request.SearchString) || request.SearchString == "")
                    .Skip((validFilter.PageNumber - 1) * request.PageSize)
                    .Take(validFilter.PageSize)
                    .ToList();
            await _context.SaveChangesAsync();
            var apps = _mapper.Map<IEnumerable<GetAllVacancyResponse>>(nextPage);
            return new PagedResponse<IEnumerable<GetAllVacancyResponse>>(apps, validFilter.PageNumber, validFilter.PageSize, (int)totalPages, (int)totalCount);

        }

        private static float CalcPages(float pageSize, float totalCount)
        {

            var totalPages = MathF.Ceiling(totalCount / pageSize);
            return totalPages;
        }
    }
}
