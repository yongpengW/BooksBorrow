using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.AppConfiguration
{
    public class ConfigurationReader:IConfigurationReader
    {
        private IConfigurationRoot _configuration = null;
        public ConfigurationReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
        }

        public string ConnectionString
        {
            get
            {
                return _configuration["connectionString"];
            }
        }
    }
}
