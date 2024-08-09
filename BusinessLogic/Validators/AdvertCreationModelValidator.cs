using BusinessLogic.Models.AdvertModels;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BusinessLogic.Validators
{
    public class AdvertCreationModelValidator : AbstractValidator<AdvertCreationModel>
    {
        public AdvertCreationModelValidator() 
        {
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price must not be null")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater or equal 0");
            RuleFor(x=>x.Title)
                .NotNull().WithMessage("Title must not be null")
                .NotEmpty().WithMessage("Title not be empty")
                .MinimumLength(4).WithMessage("The length of title must be more than 3 characters")
                .MaximumLength(256).WithMessage("The length of title must be less than 257 characters");
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Description must not be null")
                .NotEmpty().WithMessage("Description not be empty")
                .MinimumLength(10).WithMessage("The length of description must be more than 9 characters")
                .MaximumLength(3000).WithMessage("The length of description must be less than 3001 characters");
            RuleFor(x => x.CityId)
                .NotNull().WithMessage("CityId must not be null")
                .GreaterThan(0).WithMessage("CityId must be greater 0");
            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("CategoryId must not be null")
                .GreaterThan(0).WithMessage("CategoryId must be greater 0");
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("UserId must not be null")
                .NotEmpty().WithMessage("UserId not be empty");
            RuleFor(x => x.ImageFiles)
                .NotNull().WithMessage("ImageFiles must not be null")
                .NotEmpty().WithMessage("Image files not be empty");
            RuleFor(x => x.IsNew)
                 .NotNull().WithMessage("IsNew must not be null");
            RuleFor(x => x.IsVip)
                 .NotNull().WithMessage("IsVip must not be null");
            RuleFor(x => x.PhoneNumber)
               .Matches(@"^\d{3}[-\s]{1}\d{3}[-\s]{1}\d{2}[-\s]{0,1}\d{2}$")
               .WithMessage("Invalid phone number");
            RuleFor(x => x.ContactEmail)
               .EmailAddress()
               .WithMessage("Invalid email address");
            RuleFor(x => x.ContactPersone)
               .NotNull().WithMessage("Contact person must not be null")
               .NotEmpty().WithMessage("Contact person not be empty");
        }
    }
}
