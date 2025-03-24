import React, { useState } from 'react';
import api from '../services/api';  
import '../styles/AddEmployee.css';  // Importando o arquivo CSS

const AddEmployee = () => {
  const [employeeData, setEmployeeData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    documentNumber: '',
    birthDate: '',
    managerName: '',
    phones: '',
    passwordHash: '',
    role: '1', // Default role
  });

  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEmployeeData({
      ...employeeData,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      // Format phones to an array
      const phones = employeeData.phones.split(',').map(phone => phone.trim());
      
      const data = { 
        ...employeeData, 
        phones,
        role: parseInt(employeeData.role)  // Converter o valor de role para um número
      };

      const response = await api.post('/api/employees', data, 
      {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${localStorage.getItem('token')}` 
        }
      });

      if (response.status === 201) {
        alert('Empregado adicionado com sucesso!');
        setEmployeeData({
          firstName: '',
          lastName: '',
          email: '',
          documentNumber: '',
          birthDate: '',
          managerName: '',
          phones: '',
          passwordHash: '',
          role: parseInt(employeeData.role),
        });
      }
    } catch (error) {
      if (error.response && error.response.data && Array.isArray(error.response.data)) {
        const errors = error.response.data;
        // Percorrer os erros e gerar uma mensagem de alerta amigável
        const errorMessages = errors.map(err => {
          return `${err.formattedMessagePlaceholderValues.PropertyName}: ${err.errorMessage}`;
        });
        
        // Exibe as mensagens de erro
        alert(errorMessages.join("\n"));
      } else {
        alert('Erro ao criar empregado! ' + error.message);
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="add-employee-container">
      <h2>Adicionar Empregado</h2>
      {error && <p className="error-message">{error}</p>}
      <form onSubmit={handleSubmit} className="add-employee-form">
        <div className="form-group">
          <label htmlFor="firstName">Nome</label>
          <input
            type="text"
            id="firstName"
            name="firstName"
            value={employeeData.firstName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="lastName">Sobrenome</label>
          <input
            type="text"
            id="lastName"
            name="lastName"
            value={employeeData.lastName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input
            type="email"
            id="email"
            name="email"
            value={employeeData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="documentNumber">Número do Documento (CPF/CNPJ)</label>
          <input
            type="text"
            id="documentNumber"
            name="documentNumber"
            value={employeeData.documentNumber}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="birthDate">Data de Nascimento</label>
          <input
            type="date"
            id="birthDate"
            name="birthDate"
            value={employeeData.birthDate}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="managerName">Nome do Gerente</label>
          <input
            type="text"
            id="managerName"
            name="managerName"
            value={employeeData.managerName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="phones">Telefone(s) (separados por vírgula)</label>
          <input
            type="text"
            id="phones"
            name="phones"
            value={employeeData.phones}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="passwordHash">Senha</label>
          <input
            type="password"
            id="passwordHash"
            name="passwordHash"
            value={employeeData.passwordHash}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label htmlFor="role">Função</label>
          <select
            id="role"
            name="role"
            value={employeeData.role}
            onChange={handleChange}
          >
            <option value="1">Funcionário Simples</option>
            <option value="2">Líder</option>
            <option value="3">Diretor</option>
          </select>
        </div>
        <button type="submit" className="submit-btn" disabled={loading}>
          {loading ? 'Adicionando...' : 'Adicionar Empregado'}
        </button>
      </form>
    </div>
  );
};

export default AddEmployee;
