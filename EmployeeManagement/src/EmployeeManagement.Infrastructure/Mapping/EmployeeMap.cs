using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Entities;
using System.Reflection.Emit;

namespace EmployeeManagement.Infrastructure.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder
                .Property(u => u.DocumentNumber)
                .IsRequired();

            builder
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
