using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentApp.Controllers.Base;
using RecruitmentApp.Core.Vacancy.Command.Models;
using RecruitmentApp.Core.Vacancy.Query.Models;
using RecruitmentApp.Data;
using RecruitmentApp.Models;

namespace RecruitmentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] //if uncommented you have to use GetToken from User Controller to get 
    //access tken from identity server for this api then add token in authorization header 
    public class VacanciesController : AdminBaseController
    {


        public VacanciesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Vacancies
        [HttpGet]
        public async Task<IActionResult> GetVacancies([FromQuery] GetAllVacancysParameter parameter)
        {
            return Ok(await Mediator.Send(new GetAllVacancysQuery()
            {
                SearchString = parameter.SearchingWord,
                PageSize = parameter
            .PageSize,
                PageNumber = parameter.PageNumber
            }));
        }

        // GET: api/Vacancies/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);

            if (vacancy == null)
            {
                return NotFound();
            }

            return Ok(new Shared.Wrapper.Response<Vacancy>(vacancy, vacancy.Name));
        }

        // PUT: api/Vacancies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy(int id, Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacancyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Shared.Wrapper.Response<string>("updated successfully"));
        }

        // POST: api/Vacancies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostVacancy(RegisterVacancyCommand command)
        {
            var response = Mediator.Send(command);
            await _context.SaveChangesAsync();
            return Ok(response);

        }

        // DELETE: api/Vacancies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacancyExists(int id)
        {
            return _context.Vacancies.Any(e => e.Id == id);
        }
    }
}
