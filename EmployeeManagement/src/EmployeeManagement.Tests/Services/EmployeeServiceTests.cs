using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Dtos.Request;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IPhoneRepository> _phoneRepositoryMock;
        private readonly EmployeeService _employeeService;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _phoneRepositoryMock = new Mock<IPhoneRepository>();

            // Criando um DbContext In-Memory para testes
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new ApplicationDbContext(options);

            // Criando o EmployeeService com um DbContext real e um repositório mockado
            _employeeService = new EmployeeService(_dbContext, _employeeRepositoryMock.Object, _phoneRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { FirstName = "João", LastName = "Silva", Email = "joao@email.com", DocumentNumber = "12345678900", BirthDate = DateTime.Parse("1990-01-01"), Role = Role.SimpleEmployee, PasswordHash = "123", PasswordSalt = "123" },
                new Employee {FirstName = "Maria", LastName = "Souza", Email = "maria@email.com", DocumentNumber = "98765432100", BirthDate = DateTime.Parse("1985-05-05"), Role = Role.Leader, PasswordHash = "123", PasswordSalt = "123"}
            };
            _employeeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllEmployees();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].FirstName.Should().Be("João");
            _employeeRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = new Employee {FirstName = "Carlos", LastName = "Ferreira", Email = "carlos@email.com", DocumentNumber = "12345678900", BirthDate = DateTime.Parse("1980-10-10"), Role = Role.Director, PasswordHash = "123", PasswordSalt = "123" };
            var employeeId = 1;

            _employeeRepositoryMock.Setup(repo => repo.GetByIdAsync(employeeId)).ReturnsAsync(employee);

            // Act
            var result = await _employeeService.GetEmployeeById(employeeId);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Carlos");
            _employeeRepositoryMock.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
        }

        [Fact]
        public async Task AddEmployee_ShouldCallRepositoryOnce()
        {
            // Arrange
            var newEmployeeRequest = new EmployeeRequest
            {
                FirstName = "Ana",
                LastName = "Lima",
                Email = "ana@email.com",
                DocumentNumber = "11122233344",
                BirthDate = DateTime.Parse("1995-03-15"),
                Role = Role.SimpleEmployee,
                PasswordHash = "password123"
            };

            var currentUser = new Employee
            {
                FirstName = "Admin",
                LastName = "User",
                Role = Role.Director, // Apenas diretor pode cadastrar funcionários
                DocumentNumber = "123456789",
                Email = "admin@gmail.com",
                PasswordHash = "123",
                PasswordSalt = "123"
            };

            _employeeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeService.AddEmployee(newEmployeeRequest, currentUser);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Ana");
            _employeeRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnTrue_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = 1;
            var employeeDoc = "123";

            _employeeRepositoryMock.Setup(repo => repo.DeleteAsync(employeeDoc)).Returns(Task.FromResult(true));

            // Act
            var result = await _employeeService.DeleteEmployee(employeeId);

            // Assert
            result.Should().BeTrue();
            _employeeRepositoryMock.Verify(repo => repo.DeleteAsync(employeeDoc), Times.Once);
        }
    }
}
