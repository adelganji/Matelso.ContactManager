using System;
using Application.Common.Interfaces;
using Domain.Primitives.Result;

namespace Application.Commands.Contacts.DeleteContact;

public class DeleteContactCommand : ICommand<Result<bool>>
{
    public int Id { get; set; }
}
