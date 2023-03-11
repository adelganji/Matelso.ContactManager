using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace Domain.Primitives
{
    public class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false, true)
                .Build();
            return configuration;
        }
        public static T GetSection<T>(string section)
        {
            var configuration = GetConfiguration();
            var sec = configuration.GetSection(section);
            var t = sec.Get<T>();
            return t;
        }
    }
}
