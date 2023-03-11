using System.Collections.Generic;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Primitives.Result;

namespace Application.Queries.Contacts.GetAllContacts;

public class GetAllContactsQuery : IQuery<Result<List<Contact>>>
{
    
}
