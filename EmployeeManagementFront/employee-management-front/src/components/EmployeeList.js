import React, { useState, useEffect } from 'react';
import '../styles/EmployeeList.css';
import api from "../services/api";

const EmployeeList = () => {
  const [employees, setEmployees] = useState([]);
  const [editEmployee, setEditEmployee] = useState(null);

  useEffect(() => {
    api.get("/api/employees")
      .then(response => {
        setEmployees(response.data);
      })
      .catch(error => {
        console.error("There was an error fetching the employees!", error);
      });
  }, []);

  // Função para abrir o modal de edição
  const handleEdit = (employee) => {
    setEditEmployee(employee); // Definir o funcionário que será editado
  };

  // Função para salvar as edições
  const handleSave = (updatedEmployee) => {
    api.put(`/api/employees/${updatedEmployee.id}`, updatedEmployee)
      .then(response => {
        setEmployees(employees.map(emp => emp.id === updatedEmployee.id ? updatedEmployee : emp)); // Atualiza a lista de funcionários
        setEditEmployee(null); // Fecha o modal
      })
      .catch(error => {
        if (error.response && error.response.data && Array.isArray(error.response.data)) {
          const errors = error.response.data;
          // Percorrer os erros e gerar uma mensagem de alerta amigável
          const errorMessages = errors.map(err => {
            return `${err.formattedMessagePlaceholderValues.PropertyName}: ${err.errorMessage}`;
          });
          
          // Exibe as mensagens de erro
          alert(errorMessages.join("\n"));
        } else {
          alert('Erro ao salvar empregado! ' + error.message);
        }
      });
  };

  const handleDelete = (id) => {
    api.delete(`/api/employees/${id}`)
      .then(response => {
        setEmployees(employees.filter(employee => employee.id !== id)); // Remove o funcionário da lista
      })
      .catch(error => {
        alert('Erro ao excluir empregado');
      });
  };

  const formatDate = (date) => {
    if (!date) return ''; // Se a data não existir, retorne uma string vazia ou outra mensagem adequada
    const [day, month, year] = date.split('/');
    return `${year}-${month}-${day}`;
  };

  return (
    <div className="employee-list-container">
      <h1 className="employee-list-title">Lista de Funcionários</h1>
      <table className="employee-table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Email</th>
            <th>CPF/CNPJ</th>
            <th>Data de Nascimento</th>
            <th>Gerente</th>
            <th>Telefones</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((employee) => (
            <tr key={employee.documentNumber}>
              <td>{employee.firstName} {employee.lastName}</td>
              <td>{employee.email}</td>
              <td>{employee.documentNumber}</td>
              <td>{employee.birthDate ? new Date(employee.birthDate).toLocaleDateString() : new Date('01/01/0001').toLocaleDateString()}</td>
              <td>{employee.managerName}</td>
              <td>{Array.isArray(employee.phones) ? employee.phones.join(", ") : ""}</td>
              <td>
              <button className="action-button edit-button" onClick={() => handleEdit(employee)}>Editar</button>
              <button className="action-button delete-button" onClick={() => handleDelete(employee.id)}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

    {/* Modal ou Formulário de Edição */}
    {editEmployee && (
        <div className="edit-modal">
          <h3>Editar Empregado</h3>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              handleSave(editEmployee);
            }}
          >
            <input
              type="text"
              value={editEmployee.firstName}
              onChange={(e) => setEditEmployee({ ...editEmployee, firstName: e.target.value })}
            />
            <input
              type="text"
              value={editEmployee.lastName}
              onChange={(e) => setEditEmployee({ ...editEmployee, lastName: e.target.value })}
            />
            <input
              type="email"
              value={editEmployee.email}
              onChange={(e) => setEditEmployee({ ...editEmployee, email: e.target.value })}
            />
            <input
              type="text"
              value={editEmployee.phones.join(", ")}
              onChange={(e) => setEditEmployee({ ...editEmployee, phones: e.target.value.split(", ") })}
            />
            <button className="action-button save-button"  type="submit">Salvar</button>
            <button className="action-button cancel-button"  onClick={() => setEditEmployee(null)}>Cancelar</button>
          </form>
        </div>
      )}

    </div>
  );
};

export default EmployeeList;
