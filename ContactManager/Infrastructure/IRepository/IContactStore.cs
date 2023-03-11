using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Primitives.Result;

namespace Infrastructure.IRepository;

public interface IContactStore
{
    /// <summary>
    /// Get all data from PostgreSQL 
    /// </summary>
    /// <returns>All contact</returns>
    public Maybe<List<Contact>> GetAll();
    /// <summary>
    /// Get a contact from its Id from PostgreSQL
    /// </summary>
    /// <param name="id"><inheritdoc cref="int"/></param>
    public Maybe<Contact> GetById(int id);
    /// <summary>
    /// Create a new contact and insert it into PostgreSQL
    /// </summary>
    /// <param name="contact"><inheritdoc cref="Contact"/></param>
    public Maybe<int> Insert(Contact contact);
    /// <summary>
    /// Update a contact by its Id from PostgreSQL
    /// </summary>
    /// <param name="contact"><inheritdoc cref="Contact"/></param>
    public Maybe<bool> Update(Contact contact);
    /// <summary>
    /// Delete a contact from PostgreSQL
    /// </summary>
    /// <param name="id"><inheritdoc cref="int"/></param>
    public Maybe<bool> Delete(int id);
}
