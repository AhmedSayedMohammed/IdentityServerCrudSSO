using AutoMapper;
using RecruitmentApp.Core.Vacancy.Command.Models;
using RecruitmentApp.Core.Vacancy.Query.Models;

namespace RecruitmentApp.Core.Mapper
{
    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            // Commands Data Mappings
            CreateMap<Models.Vacancy, GetAllVacancyResponse>().ReverseMap();
            CreateMap<GetAllVacancysParameter, GetAllVacancysQuery>().ReverseMap();
            CreateMap<Models.Vacancy, RegisterVacancyCommand>().ReverseMap();


        }
    }
}
