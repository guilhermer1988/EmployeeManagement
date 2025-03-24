import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';  
import '../styles/Login.css'; 

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      // Enviando as credenciais para o backend
      const response = await api.post('/api/auth/login', { email, password });
      
      // Verificando se a resposta tem um token
      if (response.data.token) {
        login(response.data.token);  // Chama a função login com o token recebido
        navigate('/home'); 
      } else {
        alert('Credenciais inválidas');
      }
    } catch (error) {
      console.error("Erro de login:", error);
      alert('Erro ao fazer login. Tente novamente.');
    }
  };

  return (
    <div className="login-container">
      <div className="login-box">
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <div className="input-group">
            <label>Email</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="input-field"
            />
          </div>
          <div className="input-group">
            <label>Senha</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="input-field"
            />
          </div>
          <button type="submit" className="submit-btn">Entrar</button>
        </form>
      </div>
    </div>
  );
};

export default Login;
