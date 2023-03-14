using FluentValidation;
using System.Data;

namespace PetShelter.Api.Validators
{
    public class FundraiserValidator: AbstractValidator<Resources.Fundraiser>
    {
        public FundraiserValidator()
        {
            const int MinNameLength = 2;

            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Target).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.Owner).SetValidator(new PersonValidator());
        }
    }
}
