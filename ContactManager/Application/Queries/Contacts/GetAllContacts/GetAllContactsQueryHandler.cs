using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Primitives.Result;
using Infrastructure.IRepository;

namespace Application.Queries.Contacts.GetAllContacts;

public class GetAllContactsQueryHandler :  IQueryHandler<GetAllContactsQuery, Result<List<Contact>>>
{

    private IContactStore _contactStore;
    public GetAllContactsQueryHandler(IContactStore contactStore)
    {
        _contactStore= contactStore;
    }

    public async Task<Result<List<Contact>>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        return _contactStore.GetAll().ToResult();
    }
}
