using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;
using Domain.Primitives.Result;
using Infrastructure.IRepository;
using FluentValidation.Results;

namespace Application.Commands.Contacts.UpdateContact;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Result<bool>>
{
    private readonly IContactStore _contactStore;
    public UpdateContactCommandHandler(IContactStore contactStore)
    {
        _contactStore = contactStore;
    }

    public async Task<Result<bool>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validate = (new UpdateContactCommandValidator()).Validate(request);
        if (validate.Errors.Any())
        {
            var errStr = String.Join("; ", validate.Errors.ToList());
            return Result.Failed(false, errStr);
        }

        DateTime birthDate;
        var birthDayIsCastable = DateTime.TryParse(request.BirthDate, out birthDate);

        Contact contact = new Contact(
        id: request.Id,
        salution: request.Salution,
        firstName: request.FirstName,
        lastName: request.LastName,
        displayName: request.Displayname,
        birthDate: birthDayIsCastable? birthDate : null,
        email: request.Email,
        phoneNumber: request.PhoneNumber
        );

        //return Contact.Id;
        var res =  _contactStore.Update(contact).ToResult();
        if (res.Value)
            return Result.Success(true);
        else
            return Result.Failed(false,"item not found");

    }
}
