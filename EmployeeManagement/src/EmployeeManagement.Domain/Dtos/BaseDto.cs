using FluentValidation.Results;

namespace EmployeeManagement.Domain.Dtos
{
    public interface IBaseDto
    {
        ValidationResult Validate();
    }
}
