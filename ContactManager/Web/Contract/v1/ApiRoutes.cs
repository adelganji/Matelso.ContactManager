namespace Web.Contract.v1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string BaseApiUrl = Root + "/" + Version;

    public static class Contact
    {
        private const string Base = BaseApiUrl + "/contact";
        //public const string Test = Base + "/test";
        public const string GetContactById = Base + "/{Id}";
        public const string GetContacts = Base + "s";
        public const string CreateContact = Base + "";
        public const string UpdateContact = Base + "";
        public const string DeleteContact = Base + "/{Id}";
    }
}

