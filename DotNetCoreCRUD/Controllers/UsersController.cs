using FluentValidation;
using LuftBorn.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentApp.Data;

namespace RecruitmentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public UsersController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }
        [HttpGet("token")]
        public async Task<ActionResult> GetTokens()
        {
            //get access token for this api from identity server
            var res = Ok(_tokenService.GetToken("weatherapi.read").Result);
            return Ok(res);
        }
        [AllowAnonymous]
        [HttpPost("{email}/{password}")]
        public async Task<ActionResult> Login(string email, string password)
        {

            throw new ValidationException("Wrong email or password");



        }
    }
}
