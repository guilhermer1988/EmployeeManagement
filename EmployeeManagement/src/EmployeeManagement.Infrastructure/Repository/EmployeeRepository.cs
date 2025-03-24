using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var employeeList = await _context.Employees
                .Include(e=>e.Phones)
                .Select(e=> new Employee()
                {
                    DocumentNumber = e.DocumentNumber,
                    Email = e.Email,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    ManagerName = e.ManagerName,
                    Phones = e.Phones,
                    Role = e.Role,
                    Id = e.Id,
                    PasswordHash = e.PasswordHash,
                    PasswordSalt = e.PasswordSalt
                })
                .ToListAsync();

            return employeeList;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<bool> ExistWithSameDocumentAsync(string document)
        {
            return await _context.Employees.AnyAsync(e => e.DocumentNumber == document);
        }

        public async Task<bool> ExistWithSameEmailAsync(string email)
        {
            return await _context.Employees.AnyAsync(e => e.Email == email);
        }
    }
}
