using BusinessLogic.Models.AdvertModels;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class AdvertUpdateModelValidator: AbstractValidator<AdvertUpdateModel>
    {
        public AdvertUpdateModelValidator() 
        {
            RuleFor(x => x.Images)
               .NotNull()
               .Must(x => x != null && x.Count > 0)
               .When(x => x.ImageFiles.Count == 0, ApplyConditionTo.CurrentValidator)
               .WithMessage("No images found");
            RuleFor(x => x.Id)
                 .NotNull().WithMessage("Advert Id must not be null")
                 .GreaterThan(0).WithMessage("Advert Id must be greater 0");
            Include(new AdvertCreationModelValidator());

        }

    }
}
