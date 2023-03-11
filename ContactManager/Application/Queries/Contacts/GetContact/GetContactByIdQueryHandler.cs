using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Primitives.Result;
using Infrastructure.IRepository;

namespace Application.Queries.Contacts.GetContact;

public class GetContactByIdQueryHandler : IQueryHandler<GetContactByIdQuery, Result<Contact>>
{
    private IContactStore _contactStore;   
    public GetContactByIdQueryHandler(IContactStore contactStore)
    {
        _contactStore = contactStore;
    }

    public async Task<Result<Contact>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        return _contactStore.GetById(id: request.ContactId).ToResult();
    }
}
