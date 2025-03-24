using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interfaces.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetByIdAsync(int id);
    Task<Employee> GetByEmailAsync(string email);
    Task<bool> ExistWithSameDocumentAsync(string document);
    Task<bool> ExistWithSameEmailAsync(string document);
    Task<List<Employee>> GetAllAsync();
}
