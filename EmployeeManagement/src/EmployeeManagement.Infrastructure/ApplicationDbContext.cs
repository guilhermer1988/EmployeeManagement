using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EmployeeManagement.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Phone> Phones { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Employee>().HasData(new Employee
        {
            Id = 1, // Id fixo para evitar duplicação
            DocumentNumber = "123456789",
            FirstName = "Master",
            LastName = "Supremo",
            Email = "master_supremo_teste@gmail.com",
            BirthDate = new DateTime(2000, 3, 23),
            ManagerName = "Autônomo",
            PasswordHash = "$2a$11$pZeqWU2G3AptI.TiPfSiHuayRvwsia1QcU.8P9Z.54CbqnfkJmSve",
            PasswordSalt = "$2a$11$lkp5CTvlrmBud8RlTnqJie",
            Role = (Role)3,
            CreateAt = new DateTime(2025, 3, 23, 15, 54, 20),
            UpdateAt = DateTime.MinValue
        });

        builder.Entity<Phone>().HasData(
            new Phone { Id = 1, Number = "(31)313131-3131", EmployeeId = 1 },
            new Phone { Id = 2, Number = "+55(11)121212-1212", EmployeeId = 1 }
        );

        builder.ApplyConfiguration(new EmployeeMap());
        builder.ApplyConfiguration(new PhoneMap());
    }
}
