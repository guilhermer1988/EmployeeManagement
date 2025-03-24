using EmployeeManagement.Infrastructure;

namespace EmployeeManagement.Application.Services
{
    public abstract class BaseService
    {
        protected ApplicationDbContext _context { get; private set; }
        public BaseService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
