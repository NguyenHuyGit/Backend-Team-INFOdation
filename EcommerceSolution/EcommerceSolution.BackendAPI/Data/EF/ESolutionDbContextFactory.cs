using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EcommerceSolution.Data.EF
{
    public class ESolutionDbContextFactory : IDesignTimeDbContextFactory<ESolutionDbContext>
    {
        public ESolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var connectionString = configuration.GetConnectionString("EcommerceSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<ESolutionDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ESolutionDbContext(optionsBuilder.Options);
        }
    }
}
