using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Phone : BaseEntity
    {
        public string Number { get; set; } // Número do telefone
        public int EmployeeId { get; set; } // Chave estrangeira para o Employee
        public Employee Employee { get; set; } // Relacionamento com o Employee
    }
}
