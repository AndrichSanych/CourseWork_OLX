using BusinessLogic.Models.AccountModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(x => x.Birthdate)
                    .NotEmpty().WithMessage("Birthdate is required")
                    .LessThan(DateTime.Now).WithMessage("Birthdate must be less then today date");
            RuleFor(x => x.Name)
                     .NotEmpty().WithMessage("Name is required")
                     .Matches(@"^\p{Lu}.*").WithMessage("Name must start with uppercase letter");
            RuleFor(x => x.Surname)
                     .NotEmpty().WithMessage("Surname is required")
                     .Matches(@"^\p{Lu}.*").WithMessage("Surnam must start with uppercase letter");
            RuleFor(x => x.Email)
                     .NotEmpty().WithMessage("Email is required")
                     .EmailAddress().WithMessage("Invalid email address");
        }

    }
}
