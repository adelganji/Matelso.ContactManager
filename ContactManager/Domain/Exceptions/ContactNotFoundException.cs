using Domain.Exceptions.Base;
namespace Domain.Exceptions;

public sealed class ContactNotFoundException : NotFoundException
{
    public ContactNotFoundException(int ContactId)
        : base ($"The Contact with identifier {ContactId} was not found.")
    {
    }
}
