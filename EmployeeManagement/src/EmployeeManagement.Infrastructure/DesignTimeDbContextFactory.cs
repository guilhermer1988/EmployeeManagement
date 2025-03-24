using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName) // Caminho para o diretório raiz
                .AddJsonFile("EmployeeManagementApi/appsettings.json") // Caminho para o appsettings.json dentro de EmployeeManagementApi
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var mySqlConnectionStr = configuration.GetConnectionString("DefaultConnection");
            //optionsBuilder.UseSqlServer(mySqlConnectionStr);
            optionsBuilder.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
