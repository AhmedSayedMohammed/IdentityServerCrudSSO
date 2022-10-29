using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitmentApp.Data;

namespace RecruitmentApp.Controllers.Base
{
    public class AdminBaseController : ControllerBase
    {
        public DataContext _context;
        public IMediator _mediatorInstance;
        protected IMediator Mediator
        {
            get
            {

                var mediator = _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
                return mediator;
            }
        }
        public AdminBaseController()
        {

        }
    }
}
