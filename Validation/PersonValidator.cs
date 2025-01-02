using FluentValidation;
using WebAppProject.Models;

namespace WebAppProject.Validation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() 
        {
            RuleFor(x=>x.CreateDate).LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Must not be greater than today");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName must not be empty.")
                .Length(0, 50).WithMessage("FirstName must be between 1 and 50");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("LatName must be not empty.")
                .Length(0, 50).WithMessage("LastName must be between 1 and 50");

            RuleFor(x => x.JobPosition).NotEmpty().WithMessage("JobsPosition must be not empty.")
                .Length(0, 50).WithMessage("JobPosition must be between 1 and 50");

            RuleFor(x => x.Salary).InclusiveBetween(0, 10000).WithMessage("Salary must be between 0 and 10000");

            RuleFor(x => x.WorkExperience).NotEmpty().WithMessage("WorkExperience must not be empty.");

            RuleFor(x => x.PersonAddress).SetValidator(new AddressValidator());
        }


    }
}
