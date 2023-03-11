using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;
using MediatR;
using Domain.Primitives.Result;
using Infrastructure.IRepository;
using FluentValidation.Results;


namespace Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Result<bool>>
{
    private readonly IContactStore _contactStore;
    public DeleteContactCommandHandler(IContactStore contactStore)
    {
        _contactStore = contactStore;
    }

    public async Task<Result<bool>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validate = (new DeleteContactCommandValidator()).Validate(request);
        if (validate.Errors.Any())
        {
            var errStr = String.Join("; ", validate.Errors.ToList());
            return Result.Failed(false, errStr);
        }

        return _contactStore.Delete(request.Id).ToResult();
        
    }
}
