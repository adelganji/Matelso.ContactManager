namespace Web.Helpers;


internal class CustomServiceInjection
{
    internal static void Config(IServiceCollection services, IConfiguration Configuration)
    {
        ConfigServices(services);
        ConfigStores(services);
    }
    private static void ConfigServices(IServiceCollection services)
    {
        // if we have some services, we can put them here
    }

    private static void ConfigStores(IServiceCollection services)
    {
        // we can put other store services 
        services.AddTransient<Infrastructure.IRepository.IContactStore, Infrastructure.Repository.ContactStore>();
    }

}
