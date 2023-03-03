using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Tests;
namespace PetShelter.BusinessLayer.Validators;

public class AddDonationRequestValidator: AbstractValidator<AddDonationRequest>
{
	public AddDonationRequestValidator()
	{
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Person).NotEmpty()
         .SetValidator(new PersonValidator());
         RuleFor(x => x.Person.DateOfBirth).LessThanOrEqualTo(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
            .WithMessage("Adopter should be an adult.");
        

    }
}
