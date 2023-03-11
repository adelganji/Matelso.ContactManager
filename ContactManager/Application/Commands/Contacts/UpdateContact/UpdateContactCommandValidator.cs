using Domain.Entities;
using FluentValidation;

namespace Application.Commands.Contacts.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Salution).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("A valid email is required");
        }
    }
}