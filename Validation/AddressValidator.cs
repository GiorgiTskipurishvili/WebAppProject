using FluentValidation;
using WebAppProject.Models;

namespace WebAppProject.Validation
{
    public class AddressValidator :AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country must not be empty.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City must not be empty.");
            RuleFor(x => x.Homenumber).NotEmpty().WithMessage("HomeNumber must not be empty.");
        }
    }
}
