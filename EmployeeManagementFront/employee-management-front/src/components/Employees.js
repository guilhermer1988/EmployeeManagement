import React, { useEffect, useState } from "react";
import api from "../services/api";
import { Container, Typography, List, ListItem, ListItemText, Box, Divider } from "@mui/material";

function Employees() {
  const [employees, setEmployees] = useState([]);

  // Mapeamento do Enum Role para o nome da função
  const roleMapping = {
    1: "Funcionário Simples",
    2: "Líder",
    3: "Diretor",
  };


  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const res = await api.get("/api/employees");
        setEmployees(res.data);
      } catch (error) {
        console.error("Erro ao carregar funcionários:", error);
      }
    };
    fetchEmployees();
  }, []);

  return (
    <Container component="main" maxWidth="md">
      <Typography variant="h4" gutterBottom>
        Funcionários
      </Typography>
      <List>
        {employees.map((emp) => (
          <Box key={emp.id} sx={{ marginBottom: 2 }}>
            <ListItem sx={{ display: "flex", flexDirection: "column", paddingBottom: 1 }}>
              <ListItemText
                primary={`${emp.firstName} ${emp.lastName}`}
                secondary={`Email: ${emp.email}`}
              />
              <ListItemText primary={`Documento: ${emp.documentNumber}`} />
              <ListItemText primary={`Data de Nascimento: ${new Date(emp.birthDate).toLocaleDateString()}`} />
              <ListItemText primary={`Nome do Gerente: ${emp.managerName}`} />
              <ListItemText primary={`Telefones: ${emp.phones.map(phone.number ? phone.number : "").join(", ")}`} />
              <ListItemText primary={`Função: ${roleMapping[emp.role] || "Desconhecido"}`} />
            </ListItem>
            <Divider sx={{ marginBottom: 2 }} />
          </Box>
        ))}
      </List>
    </Container>
  );
}

export default Employees;
