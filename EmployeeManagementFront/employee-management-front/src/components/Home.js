import React from 'react';
import { Link } from 'react-router-dom';
import '../styles/Home.css';  // Importe o arquivo CSS

const Home = () => {
  return (
    <div>
       <div className="home-container">
      <h1 className="home-title">Bem-vindo ao Sistema de Funcionários</h1>
      <nav className="home-nav">
        <ul>
          <li className="nav-item">
            <Link to="/employeeList" className="nav-link">Editar Funcionários</Link>
          </li>
          <li className="nav-item">
            <Link to="/addEmployees" className="nav-link">Adicionar Funcionário</Link>
          </li>
        </ul>
      </nav>
    </div>
    </div>
  );
};

export default Home;
