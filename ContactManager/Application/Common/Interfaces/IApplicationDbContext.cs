using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common;

// This file will be used if have the EF.
public interface IApplicationDbContext : IDisposable
{
    //DbSet<Contact> Contacts { get; set; } 
    //Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
