using System.Data;
using Domain.Entities;
using Domain.Primitives.Result;
using Infrastructure.IRepository;
using Npgsql;

namespace Infrastructure.Repository;

public class ContactStore : IContactStore
{
    private string cs;
    public ContactStore()
    {
        cs = DataBase.DataBase.Config.DefaultConnectionString;
    }

    public Maybe<List<Contact>> GetAll()
    {
        List<Contact> contacts = new List<Contact>();

        using (var connection = new NpgsqlConnection(cs))
        {
            connection.Open();
            string sql = "SELECT * FROM Contact ";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contact contact = new Contact(
                        id: reader.GetInt32("Id"),
                        salution: reader.GetString(reader.GetOrdinal("salution")),
                        firstName: reader.GetString(reader.GetOrdinal("firstname")),
                        lastName: reader.GetString(reader.GetOrdinal("lastname")),
                        displayName: reader.GetString(reader.GetOrdinal("displayname")),
                        birthDate: reader.IsDBNull(reader.GetOrdinal("birthdate")) ? null : reader.GetDateTime(reader.GetOrdinal("birthdate")),
                        creationTimestamp: DateTimeUnixTime(reader.GetDateTime(reader.GetOrdinal("creationtimestamp"))),
                        lastChangeTimestamp: reader.IsDBNull(reader.GetOrdinal("lastchangetimestamp")) ? null : DateTimeUnixTime(reader.GetDateTime(reader.GetOrdinal("lastchangetimestamp"))),
                        email: reader.GetString(reader.GetOrdinal("email")),
                        phoneNumber: reader.IsDBNull(reader.GetOrdinal("phonenumber")) ? null : reader.GetString(reader.GetOrdinal("phonenumber"))
                            );
                        contacts.Add(contact);
                    }
                }
            }
        }

        return Maybe<List<Contact>>.From(contacts);

    }

    public Maybe<Contact> GetById(int id)
    {
        try
        {
            using (var connection = new NpgsqlConnection(cs))
            {
                connection.Open();

                var command = new NpgsqlCommand("SELECT * FROM Contact WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var contact = new Contact(
                            id: reader.GetInt32("Id"),
                            salution: reader.GetString(reader.GetOrdinal("Salution")),
                            firstName: reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName: reader.GetString(reader.GetOrdinal("LastName")),
                            displayName: reader.GetString(reader.GetOrdinal("Displayname")),
                            birthDate: reader.IsDBNull(reader.GetOrdinal("Birthdate")) ? null : reader.GetDateTime(reader.GetOrdinal("Birthdate")),
                            creationTimestamp: DateTimeUnixTime(reader.GetDateTime(reader.GetOrdinal("creationtimestamp"))),
                            lastChangeTimestamp: reader.IsDBNull(reader.GetOrdinal("lastchangetimestamp")) ? null : DateTimeUnixTime(reader.GetDateTime(reader.GetOrdinal("lastchangetimestamp"))),
                            email: reader.GetString(reader.GetOrdinal("Email")),
                            phoneNumber: reader.IsDBNull(reader.GetOrdinal("phonenumber")) ? null : reader.GetString(reader.GetOrdinal("phonenumber"))
                            );
                        connection.Close();
                        return Maybe<Contact>.From(contact);
                    }
                    else
                    {
                        connection.Close();
                        return Maybe<Contact>.None;
                        //return null;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    public Maybe<int> Insert(Contact contact)
    {
        int insertedId = 0;
        using (NpgsqlConnection connection = new NpgsqlConnection(cs))
        {
            connection.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO Contact (salution, firstname, lastname, displayname, birthdate, email, phonenumber) VALUES (@salution, @firstname, @lastname, @displayname, @birthdate, @email, @phonenumber) returning id";
                cmd.Parameters.AddWithValue("salution", contact.Salution);
                cmd.Parameters.AddWithValue("firstname", contact.FirstName);
                cmd.Parameters.AddWithValue("lastname", contact.LastName);
                cmd.Parameters.AddWithValue("displayname", contact.Displayname);
                cmd.Parameters.AddWithValue("birthdate", contact.BirthDate == null ? DBNull.Value : contact.BirthDate);
                cmd.Parameters.AddWithValue("email", contact.Email);
                cmd.Parameters.AddWithValue("phonenumber", contact.PhoneNumber == null ? DBNull.Value : contact.PhoneNumber);
                
                insertedId = (int)cmd.ExecuteScalar();
            }
            connection.Close();
        }

        return Maybe<int>.From(insertedId);
    }

    public Maybe<bool> Update(Contact contact)
    {
        int executed = 0;
        using (NpgsqlConnection connection = new NpgsqlConnection(cs))
        {
            connection.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "UPDATE Contact SET salution = @salution, firstname = @firstname, lastname = @lastname, displayname = @displayname, birthdate = @birthdate, email = @email, phonenumber = @phonenumber,"
                                    + " lastchangetimestamp = CURRENT_TIMESTAMP WHERE id = @id";

                cmd.Parameters.AddWithValue("salution", contact.Salution);
                cmd.Parameters.AddWithValue("firstname", contact.FirstName);
                cmd.Parameters.AddWithValue("lastname", contact.LastName);
                cmd.Parameters.AddWithValue("displayname", contact.Displayname);
                cmd.Parameters.AddWithValue("birthdate", contact.BirthDate==null ? DBNull.Value: contact.BirthDate);
                cmd.Parameters.AddWithValue("email", contact.Email);
                cmd.Parameters.AddWithValue("phonenumber", contact.PhoneNumber == null ? DBNull.Value : contact.PhoneNumber);
                cmd.Parameters.AddWithValue("id", contact.Id);
                executed = cmd.ExecuteNonQuery();
            }
            
            connection.Close();
        }
        return Maybe<bool>.From(executed > 0);
    }

    public Maybe<bool> Delete(int id)
    {
        int executed = 0;
        using (var connection = new NpgsqlConnection(cs))
        {
            connection.Open();
            using (var command = new NpgsqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "DELETE FROM Contact WHERE id = @id";
                command.Parameters.AddWithValue("id", id);
                executed = command.ExecuteNonQuery();
                connection.Close();

            }
        }
        return Maybe<bool>.From(executed > 0);
        //return Maybe<bool>.From(true);
    }


    #region private methods

    /// <summary>
    /// Change datetime to timestamp
    /// </summary>
    /// <param name="dt"><inheritdoc cref="DataTable"/></param>
    /// <returns> 'long' result which is the total seconds from 1970/01/01 up to the given date </returns>
    private long DateTimeUnixTime(DateTime dt)
    {
        var timeSpan = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
        return (long)timeSpan.TotalSeconds;
    }

    # endregion prvate methods

}
