using System.Data;

namespace EmployeeManagement.Domain.Entities;

public class Employee : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string DocumentNumber { get; set; } // CPF/CNPJ
    public DateTime BirthDate { get; set; }
    public string ManagerName { get; set; }
    public List<Phone> Phones { get; set; }
    public string PasswordHash { get; set; } // Hashed password
    public string PasswordSalt { get; set; } // Salt used to hash the password
    public Role Role { get; set; }
}

public enum Role
{
    SimpleEmployee = 1,
    Leader = 2,
    Director = 3,
}