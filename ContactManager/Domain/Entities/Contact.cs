using Domain.Primitives;

namespace Domain.Entities;

public sealed class Contact : BaseEntity
{
    public static Contact emptyModel = new Contact();
    private string _displayName;
    public Contact(int id, string salution, string firstName, string lastName, string displayName, DateTime? birthDate, string email, string phoneNumber) :
        base(id)
    {
        Salution = salution;
        FirstName = firstName;
        LastName = lastName;
        _displayName = displayName;
        BirthDate = birthDate;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    public Contact(int id, string salution, string firstName, string lastName, string displayName, DateTime? birthDate, long creationTimestamp, long? lastChangeTimestamp, string email, string phoneNumber) :
        base(id)
    {
        Salution = salution;
        FirstName = firstName;
        LastName = lastName;
        _displayName = displayName;
        BirthDate = birthDate;
        CreationTimestamp = creationTimestamp;
        LastChangeTimestamp = lastChangeTimestamp;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    private Contact()
    {
    }
    public string Salution { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Displayname
    {
        get
        {
            if (string.IsNullOrEmpty(_displayName))
            {
                _displayName = $"{Salution} {FirstName} {LastName}";
            }
            return _displayName;
        }
        set
        {
            _displayName = value;
        }
    }
    public DateTime? BirthDate { get; private set; }
    public long CreationTimestamp { get; private set; }
    public long? LastChangeTimestamp { get; private set; }
    public bool NotifyHasBirthdaySoon
    {
        get
        {
            if (BirthDate == null)
                return false;

            int month = BirthDate.Value.Month, day = BirthDate.Value.Day;
            DateTime nextBirthday;
            //if birthday is in a leap year
            if (month == 2 && day == 29)
            {
                try
                {
                    nextBirthday = new DateTime(DateTime.Today.Year, month, day);
                }
                catch (Exception ex)
                {
                    month = 3;
                    day = 1;
                }
            }
            nextBirthday = new DateTime(DateTime.Today.Year, month, day);
            var daysToBirthDay = (nextBirthday - DateTime.Today).Days;
            if (daysToBirthDay <= 14 && daysToBirthDay >= 0)
            {
                return true;
            }
            return false;
        }
    }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
}
