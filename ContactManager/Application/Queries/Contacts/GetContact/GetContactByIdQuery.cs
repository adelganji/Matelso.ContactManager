using Application.Common.Interfaces;
using Domain.Primitives.Result;
using Domain.Entities;

namespace Application.Queries.Contacts.GetContact;

public class GetContactByIdQuery : IQuery<Result<Contact>>
{
    public int ContactId { get; set; }
}
