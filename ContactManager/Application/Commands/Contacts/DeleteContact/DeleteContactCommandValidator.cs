using Domain.Entities;
using FluentValidation;

namespace Application.Commands.Contacts.DeleteContact
{
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}