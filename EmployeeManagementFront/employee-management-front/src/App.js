import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';  // Importando corretamente o Routes e Route
import { AuthProvider } from './context/AuthContext';  // O contexto de autenticação
import Header from './components/Header';
import Login from './components/Login';  
import PrivateRoute from './components/PrivateRoute';  // A rota privada
import Home from './components/Home';  
import EmployeeForm from './components/EmployeeForm';  
import EmployeeList from './components/EmployeeList';  
import Employees from './components/Employees';  
import AddEmployee from './components/AddEmployee';  

const App = () => {
  
  return (
    <AuthProvider>  {/* Envolvendo a aplicação com o AuthProvider */}
      <Router>  {/* O Router que envolve a aplicação toda */}
        <Header />  {/* O Header será renderizado para todos os componentes que passarem pela verificação de autenticação */}
        
        {/* O componente Routes deve envolver todas as <Route> */}
        <Routes>
          <Route path="/" element={<Login />} />
          
          {/* Rota protegida */}
          {/* <Route element={<PrivateRoute />}> */}
            <Route path="/home" element={<Home />} />  {/* Rota protegida, só acessível se autenticado */}
            <Route path="/employees" element={<Employees />} />  {/* Rota protegida, só acessível se autenticado */}
            <Route path="/employeeList" element={<EmployeeList />} />  {/* Rota protegida, só acessível se autenticado */}
            <Route path="/employeeForm" element={<EmployeeForm />} />  {/* Rota protegida, só acessível se autenticado */}
            <Route path="/employeeList" element={<EmployeeList />} />  {/* Rota protegida, só acessível se autenticado */}
            <Route path="/addEmployees" element={<AddEmployee />} />  {/* Rota protegida, só acessível se autenticado */}
          {/* </Route> */}
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
