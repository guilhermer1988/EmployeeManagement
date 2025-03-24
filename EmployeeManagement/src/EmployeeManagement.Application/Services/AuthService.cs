using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeRepository _employeeRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor,
            IEmployeeRepository employeeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee?> GetCurrentUser()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null)
            {
                return null;
            }

            var userId = int.Parse(userIdClaim);

            var user = await _employeeRepository.GetByIdAsync(userId);

            return user;
        }
    }
}
