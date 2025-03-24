using BCrypt.Net;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services
{
    internal static class PasswordService
    {
        public static bool VerifyPassword(Employee employee, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash);
        }

        public static (string, string) GetHashedPaswordAndSalt(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return (hashedPassword, salt);
        }
    }
}
