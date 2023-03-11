using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;
using Domain.Primitives.Result;
using Infrastructure.IRepository;
using FluentValidation.Results;

namespace Application.Commands.Contacts.CreateContact;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Result<int>>
{
    private readonly IContactStore _contactStore;
    public CreateContactCommandHandler(IContactStore contactStore)
    {
        _contactStore = contactStore;
    }

    public async Task<Result<int>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        CreateContactCommandValidator validator = new CreateContactCommandValidator();
        ValidationResult validate = validator.Validate(request);

        if (validate.Errors.Any())
        {
            var errStr = String.Join("; ", validate.Errors.ToList());
            return Result.Failed(0, errStr);
        }

        DateTime birthDate;
        var birthDayIsCastable = DateTime.TryParse(request.BirthDate, out birthDate);

        Contact contact = new Contact(0,
        salution: request.Salution,
        firstName: request.FirstName,
        lastName: request.LastName,
        displayName: request.Displayname,
        //birthDate: request.BirthDate,
        birthDate: birthDayIsCastable ? birthDate : null,
        email: request.Email,
        phoneNumber: request.PhoneNumber
        );


        //return Contact.Id;
        return _contactStore.Insert(contact).ToResult();
    }
}
