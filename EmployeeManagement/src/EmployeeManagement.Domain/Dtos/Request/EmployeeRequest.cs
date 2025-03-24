using EmployeeManagement.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace EmployeeManagement.Domain.Dtos.Request
{
    public class EmployeeRequest : IBaseDto
    {
        public int? Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; } // CPF/CNPJ
        public DateTime BirthDate { get; set; }
        public string ManagerName { get; set; }
        public List<string> Phones { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

        public ValidationResult Validate()
        {
            var validationResult = new EmployeeRequestValidation().Validate(this);
            return validationResult;
        }
    }

    public class EmployeeRequestValidation : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("ValidationFirstNameEmpty");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("ValidationLastNameEmpty");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("ValidationEmailEmpty");
            RuleFor(x => x.DocumentNumber).NotNull().NotEmpty().WithMessage("ValidationDocumentNumberEmpty");
            RuleFor(x => x.PasswordHash).NotNull().NotEmpty().WithMessage("ValidationPasswordEmpty");
            RuleFor(x => x.BirthDate)
                .Must(BeAtLeast18YearsOld).WithMessage("EmployeeMustBeAdult");
        }

        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (DateTime.Today.Month < dateOfBirth.Month ||
                (DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day < dateOfBirth.Day))
            {
                age--;
            }
            return age >= 18;
        }
    }
}
