import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const EmployeeForm = () => {
  const { id } = useParams(); // Obtém o id do funcionário se houver
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [email, setEmail] = useState('');

  useEffect(() => {
    if (id) {
      // Simulação de busca de dados de um funcionário para edição
      const employee = { name: 'John Doe', email: 'john.doe@example.com' }; // Aqui você usaria uma API real
      setName(employee.name);
      setEmail(employee.email);
    }
  }, [id]);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (id) {
      // Simulação de atualização de funcionário
      console.log('Funcionário atualizado', { name, email });
    } else {
      // Simulação de criação de novo funcionário
      console.log('Novo funcionário criado', { name, email });
    }
    navigate('/employees');
  };

  return (
    <div className="employee-form-container">
      <h2>{id ? 'Editar Funcionário' : 'Cadastrar Funcionário'}</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Nome</label>
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Email</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <button type="submit">{id ? 'Salvar' : 'Cadastrar'}</button>
      </form>
    </div>
  );
};

export default EmployeeForm;
