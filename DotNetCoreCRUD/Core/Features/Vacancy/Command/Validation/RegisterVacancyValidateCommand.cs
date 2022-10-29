using FluentValidation;
using RecruitmentApp.Core.Vacancy.Command.Models;


namespace RecruitmentApp.Core.Vacancy.Command.Validation
{
    public class RegisterVacancyValidateCommand : AbstractValidator<RegisterVacancyCommand>
    {



        #region Constructor(s)
        public RegisterVacancyValidateCommand()
        {

            ApplyValidationRules();
        }
        #endregion

        #region Basic Validation Methods
        private void ApplyValidationRules()
        {
            RuleFor(x => x.JobCategory).NotEmpty().WithMessage("Job category Can't be empty");
            RuleFor(x => x.Skills).NotEmpty().WithMessage("Skills Can't be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Can't be empty");
            RuleFor(x => x.Responsibilities).NotEmpty().WithMessage("Responsibilities Can't be empty");
        }
        #endregion
        #region Custom Validation Methods


        #endregion
    }
}
