using EmployeeManagement.Domain.Dtos.Request;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(EmployeeRequest employee, Employee currentuser);
        Task<Employee?> GetEmployeeById(int id);
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> UpdateEmployee(EmployeeRequest employee);
        Task<bool> DeleteEmployee(int id);
    }
}
