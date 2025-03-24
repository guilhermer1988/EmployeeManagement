# Employee Management System

## Descrição do Projeto

Este projeto consiste em um sistema de gerenciamento de funcionários com backend em .NET 8 e frontend em React. O projeto utiliza Docker para facilitar a execução e o gerenciamento do ambiente de desenvolvimento.

## Requisitos

Antes de executar o projeto, é necessário ter o Docker instalado em sua máquina. Você pode baixar o Docker [aqui](https://www.docker.com/products/docker-desktop).

## Estrutura do projeto
├── EmployeeManagement
    ├── .vs
    │   ├── EmployeeManagement
    │   │   ├── DesignTimeBuild
    │   │   │   └── .dtbcache.v2
    │   │   ├── FileContentIndex
    │   │   │   ├── 1159f080-c83f-4720-8b0c-8ae8e2609b36.vsidx
    │   │   │   ├── 29ecd1c7-e4af-4a13-bb3d-a05d7ca45f97.vsidx
    │   │   │   ├── 3f1e81ad-b313-4ea3-8a46-5c2239b776d9.vsidx
    │   │   │   ├── 6f75c322-91d1-42a7-9ab8-46193130ea7d.vsidx
    │   │   │   └── b3cc22eb-536d-47d9-8f0f-18a5d9344852.vsidx
    │   │   ├── config
    │   │   │   └── applicationhost.config
    │   │   └── v17
    │   │   │   ├── .futdcache.v2
    │   │   │   ├── .suo
    │   │   │   ├── DocumentLayout.backup.json
    │   │   │   └── DocumentLayout.json
    │   └── ProjectEvaluation
    │   │   ├── employeemanagement.metadata.v9.bin
    │   │   ├── employeemanagement.projects.v9.bin
    │   │   └── employeemanagement.strings.v9.bin
    ├── EmployeeManagement.sln
    └── src
    │   ├── Dockerfile
    │   ├── EmployeeManagement.Application
    │       ├── EmployeeManagement.Application.csproj
    │       └── Services
    │       │   ├── AuthService.cs
    │       │   ├── BaseService.cs
    │       │   ├── EmployeeService.cs
    │       │   └── PasswordService.cs
    │   ├── EmployeeManagement.Domain
    │       ├── Dtos
    │       │   ├── BaseDto.cs
    │       │   └── Request
    │       │   │   └── EmployeeRequest.cs
    │       ├── EmployeeManagement.Domain.csproj
    │       ├── Entities
    │       │   ├── BaseEntity.cs
    │       │   ├── Employee.cs
    │       │   └── Phone.cs
    │       └── Interfaces
    │       │   ├── Repositories
    │       │       ├── IEmployeeRepository.cs
    │       │       ├── IPhoneRepository.cs
    │       │       └── IRepository.cs
    │       │   └── Services
    │       │       ├── IAuthService.cs
    │       │       └── IEmployeeService.cs
    │   ├── EmployeeManagement.Infrastructure
    │       ├── ApplicationDbContext.cs
    │       ├── DesignTimeDbContextFactory.cs
    │       ├── EmployeeManagement.Infrastructure.csproj
    │       ├── EmployeeManagement.Infrastructure.csproj.Backup.tmp
    │       ├── Mapping
    │       │   ├── EmployeeMap.cs
    │       │   └── PhoneMap.cs
    │       ├── Migrations
    │       │   ├── 20250324054021_InitialMigration.Designer.cs
    │       │   ├── 20250324054021_InitialMigration.cs
    │       │   ├── 20250324060641_InsertDefaultUser.Designer.cs
    │       │   ├── 20250324060641_InsertDefaultUser.cs
    │       │   └── ApplicationDbContextModelSnapshot.cs
    │       └── Repository
    │       │   ├── BaseRepository.cs
    │       │   ├── EmployeeRepository.cs
    │       │   └── PhoneRepository.cs
    │   ├── EmployeeManagement.Tests
    │       ├── EmployeeManagement.Tests.csproj
    │       ├── Services
    │       │   └── EmployeeServiceTests.cs
    │       └── UnitTest1.cs
    │   └── EmployeeManagementApi
    │       ├── Controller
    │           ├── AuthController.cs
    │           └── EmployeeController.cs
    │       ├── EmployeeManagementApi.csproj
    │       ├── EmployeeManagementApi.csproj.user
    │       ├── EmployeeManagementApi.http
    │       ├── Program.cs
    │       ├── Properties
    │           └── launchSettings.json
    │       ├── appsettings.Development.json
    │       └── appsettings.json
├── EmployeeManagementFront
    └── employee-management-front
    │   ├── Dockerfile
    │   ├── README.md
    │   ├── nginx.conf
    │   ├── package-lock.json
    │   ├── package.json
    │   ├── public
    │       ├── favicon.ico
    │       ├── index.html
    │       ├── logo192.png
    │       ├── logo512.png
    │       ├── manifest.json
    │       └── robots.txt
    │   └── src
    │       ├── App.css
    │       ├── App.js
    │       ├── App.test.js
    │       ├── components
    │           ├── AddEmployee.js
    │           ├── EmployeeForm.js
    │           ├── EmployeeList.js
    │           ├── Employees.js
    │           ├── Header.js
    │           ├── Home.js
    │           ├── Login.js
    │           └── PrivateRoute.js
    │       ├── context
    │           └── AuthContext.js
    │       ├── index.css
    │       ├── index.js
    │       ├── logo.svg
    │       ├── reportWebVitals.js
    │       ├── services
    │           ├── api.js
    │           └── auth.js
    │       ├── setupTests.js
    │       └── styles
    │           ├── AddEmployee.css
    │           ├── EmployeeList.css
    │           ├── Header.css
    │           ├── Home.css
    │           ├── Login.css
    │           └── global.css
└── docker-compose.yml

## Instruções para Executar o Projeto

1. **Clone o repositório**

   Clone o repositório para o seu ambiente local:

   ```bash
   git clone https://github.com/usuario/nome-do-repositorio.git
   cd nome-do-repositorio
   
Navegue até a pasta onde o arquivo docker-compose.yml está localizado. Geralmente, essa pasta é a raiz do projeto ou onde o backend e frontend estão configurados para serem executados com Docker.

Esse comando irá construir as imagens e iniciar os containers necessários para o backend e frontend.

Acessar o Sistema
Após o Docker Compose ter finalizado a construção e inicialização, o sistema estará disponível. Para acessar a aplicação, abra o navegador e vá até:
Backend (API): http://localhost:5018
Frontend (Aplicação): http://localhost:8082

Login
O sistema possui um usuário pré-configurado para testes:
Usuário: master_supremo_teste@gmail.com
Senha: Senha@123
Após fazer login, você poderá navegar pela aplicação.

Acessar o Swagger
Para acessar a documentação da API via Swagger, vá até:
http://localhost:5018/swagger
O Swagger fornecerá informações sobre os endpoints disponíveis e como interagir com a API.

Considerações Finais
Certifique-se de que o Docker esteja corretamente configurado e que os containers sejam executados sem problemas.
