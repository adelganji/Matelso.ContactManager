using System;
using Application.Common.Interfaces;
using Domain.Primitives.Result;

namespace Application.Commands.Contacts.CreateContact;

public class CreateContactCommand : ICommand<Result<int>>
{
    public string Salution { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Displayname { get; set; }
    public string BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
