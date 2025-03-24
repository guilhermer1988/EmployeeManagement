using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Dtos.Request;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Domain.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPhoneRepository _phoneRepository;

        public EmployeeService(ApplicationDbContext context, 
            IEmployeeRepository employeeRepository,
            IPhoneRepository phoneRepository) : base(context)
        {
            _employeeRepository = employeeRepository;
            _phoneRepository = phoneRepository;
        }

        public async Task<Employee> AddEmployee(EmployeeRequest request, Employee currentuser)
        {
            await CreateValidation(request, currentuser);

            (string, string) hashedPaswordAndSalt = CreatePassword(request.PasswordHash);

            Employee employee = new Employee()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                DocumentNumber = request.DocumentNumber,
                Email = request.Email,
                ManagerName = request.ManagerName,
                PasswordHash = hashedPaswordAndSalt.Item1,
                PasswordSalt = hashedPaswordAndSalt.Item2,
                Role = request.Role,
            };

            List<Phone> phones = request.Phones.Select(phone => new Phone
            {
                Number = phone,
                Employee = employee 
            }).ToList();

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _employeeRepository.AddAsync(employee);
                await _phoneRepository.AddRangeAsync(phones);

                transaction.Commit();
            }

            return employee;
        }

        private async Task CreateValidation(EmployeeRequest request, Employee currentuser)
        {
            if (await ExistDocument(request.DocumentNumber))
                throw new ArgumentException("Alredy exist Employee with the same document in the data base.");

            if (await ExistEmail(request.Email))
                throw new ArgumentException("Alredy exist Employee with the same Email in the data base.");

            if (!HasPermission((int)request.Role, (int)currentuser.Role))
                throw new ArgumentException("You do not have permission to create this user.");
        }

        private bool HasPermission(int currentUser, int employeeRole)
        {
            return (currentUser < employeeRole);
        }

        private async Task<bool> ExistEmail(string email)
        {
            return await _employeeRepository.ExistWithSameEmailAsync(email);
        }

        private async Task<bool> ExistDocument(string documentNumber)
        {
            return await _employeeRepository.ExistWithSameDocumentAsync(documentNumber);
        }

        private static (string, string) CreatePassword(string password)
        {
            (string, string) hashedPaswordAndSalt = PasswordService.GetHashedPaswordAndSalt(password);
            return hashedPaswordAndSalt;
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee is null)
                return false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _employeeRepository.DeleteAsync(employee.DocumentNumber);

                transaction.Commit();
            }

            return true;
        }

        public async Task<Employee> UpdateEmployee(EmployeeRequest request)
        {
            Employee employeeEntity = await _employeeRepository.GetByIdAsync(request.Id.Value) ?? throw new ArgumentException("Employee must be an adult.");

            (string, string) hashedPaswordAndSalt = CreatePassword(request.PasswordHash);

            Employee employee = new Employee()
            {
                Id = request.Id.Value,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                DocumentNumber = request.DocumentNumber,
                Email = request.Email,
                ManagerName = request.ManagerName,
                PasswordHash = hashedPaswordAndSalt.Item1,
                PasswordSalt = hashedPaswordAndSalt.Item2,
                Role = request.Role,
                UpdateAt = DateTime.Now
            };

            List<Phone> newPhones = await UpdatePhones(request);

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _employeeRepository.UpdateAsync(employee);
                await _phoneRepository.AddRangeAsync(newPhones);

                transaction.Commit();
            }

            return employee;
        }

        private async Task<List<Phone>> UpdatePhones(EmployeeRequest request)
        {
            var currentPhones = await _context.Set<Phone>().Where(p => p.EmployeeId == request.Id.Value).ToListAsync();
            _context.Set<Phone>().RemoveRange(currentPhones);

            var newPhones = request.Phones.Select(phone => new Phone
            {
                Number = phone,
                EmployeeId = request.Id.Value,
            }).ToList();
            return newPhones;
        }
    }

}
