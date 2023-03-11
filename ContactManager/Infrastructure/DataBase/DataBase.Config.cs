using System.Dynamic;

namespace Infrastructure.DataBase;

public static partial class DataBase
{
    public class Config
	{
		private Config() { }

		public static string DefaultConnectionString;

		private static readonly Lazy<Config> _default = new Lazy<Config>(() => new Config());

		public static Config Default => _default.Value;

		private readonly IDictionary<string, object> _connectionStrings = new ExpandoObject();
		public dynamic ConnectionStrings => _connectionStrings;

		public Config AddConnectionString(string name, string connectionString)
		{
			if (_connectionStrings.ContainsKey(name))
				_connectionStrings[name] = connectionString;
			else
				_connectionStrings.Add(name, connectionString);
			return this;
		}
	}
}
