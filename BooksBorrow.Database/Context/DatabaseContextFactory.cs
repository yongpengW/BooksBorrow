using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Database.Context
{
    /// <summary>
    /// DatabaseContextFactory 只用于数据迁移
    /// </summary>
    public class DatabaseContextFactory: IDesignTimeDbContextFactory<DatabaseContext>
    {
        private static string _connectionString;

        public DatabaseContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }
            //_connectionString = "Data Source=172.27.0.175;Initial Catalog=test;User ID=sa;Password=a@12345;MultipleActiveResultSets=True;";
            return new DatabaseContext(_connectionString);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            _connectionString = configuration["connectionString"];
        }
    }
}
