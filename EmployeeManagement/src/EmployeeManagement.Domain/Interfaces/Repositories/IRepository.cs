using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<Boolean> AddRangeAsync(IEnumerable<T> objs);
        Task UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(string id);
        Task SaveChangesAsync();
    }
}
