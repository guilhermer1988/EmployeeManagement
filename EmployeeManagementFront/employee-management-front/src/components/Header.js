import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext'; // Importando o contexto de autenticação
import '../styles/Header.css'; // Importando o CSS

const Header = () => {
  const { logout } = useAuth(); // Obtendo logout do contexto

  const isAuthenticated = localStorage.getItem('authenticated');

  const handleLogout = () => {
    logout(); // Chama a função de logout
    window.location.href = "/"; // Redireciona para a página de login após o logout
  };
  
  // Se o usuário não estiver autenticado, não renderiza o Header
  if (isAuthenticated === 'false') {
    return null;
  }

  return (
    <header className="header">
      <div className="logo">
        <h1>Employee Management</h1>
      </div>
      <nav>
        <ul>
          <li>
            <Link to="/home">Home</Link>
          </li>
          <li>
            <Link to="/employees">Empregados</Link>
          </li>
          <li>
            <button onClick={handleLogout} className="logout-button">Logout</button>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;
