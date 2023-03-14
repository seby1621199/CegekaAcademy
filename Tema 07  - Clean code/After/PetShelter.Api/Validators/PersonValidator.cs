using FluentValidation;

namespace PetShelter.Api.Validators
{
    public class PersonValidator : AbstractValidator<Resources.Person>
    {
        private const int IdNumberLength = 13;
        private const int MinNameLength = 2;

        public PersonValidator()
        {
            RuleFor(x => x.IdNumber).Length(IdNumberLength);
            RuleFor(x => x.Name).NotEmpty().MinimumLength(MinNameLength);
        }
    }
}
