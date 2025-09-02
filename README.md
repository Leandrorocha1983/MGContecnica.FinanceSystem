# MG Contecnica - Sistema de Controle Financeiro

Sistema de controle financeiro desenvolvido em .NET 8 com arquitetura limpa e frontend Angular, permitindo gestão completa de receitas e despesas com categorização, relatórios financeiros e interface moderna.

## 🚀 Tecnologias Utilizadas

### Backend
- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core** - ORM com SQLite
- **Serilog** - Logging estruturado
- **xUnit** - Testes unitários
- **Swagger/OpenAPI** - Documentação da API
- **FluentValidation** - Validações
- **SQLite** - Banco de dados

### Frontend
- **Angular 17** - Framework SPA
- **TypeScript** - Linguagem de programação
- **SCSS** - Estilização
- **RxJS** - Programação reativa
- **Angular Material** - Componentes UI

## 📋 Funcionalidades Implementadas

### Backend - API REST
- ✅ **CRUD completo de transações**
  - GET /api/transacoes (com filtros)
  - GET /api/transacoes/{id}
  - POST /api/transacoes
  - PUT /api/transacoes/{id}
  - DELETE /api/transacoes/{id}
  - GET /api/transacoes/paged (paginação)

- ✅ **CRUD básico de categorias**
  - GET /api/categorias
  - POST /api/categorias

- ✅ **Relatórios financeiros**
  - GET /api/relatorios/resumo
  - GET /api/relatorios/por-categoria

### Frontend - Dashboard Angular
- ✅ **Dashboard profissional** com resumo financeiro
- ✅ **Cards informativos** (receitas, despesas, saldo)
- ✅ **Listagem de transações** e categorias
- ✅ **Design responsivo** e moderno
- ✅ **Integração completa** com API via proxy

### Melhorias Profissionais
- ✅ **Logging estruturado** com Serilog
- ✅ **Testes unitários** automatizados
- ✅ **Paginação** para performance
- ✅ **Arquitetura limpa** (4 camadas)
- ✅ **Validações de negócio** rigorosas
- ✅ **Documentação completa** via Swagger

## ⚖️ Validações de Negócio

- **Valor**: Deve ser maior que zero
- **Data**: Não pode ser futura
- **Categoria**: Deve existir e estar ativa
- **Descrição**: Campo obrigatório

## 🏗️ Arquitetura

src/
├── MGContecnica.API/          # Controllers e configurações
├── MGContecnica.Application/  # Services, DTOs e validações
├── MGContecnica.Domain/       # Entidades, interfaces e modelos
├── MGContecnica.Infrastructure/# Repositories e DbContext
└── MGContecnica.WebApp/       # Frontend Angular
## ⚡ Como Executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)

### 1. Clone o repositório
```bash
git clone https://github.com/Leandrorocha1983/MGContecnica.FinanceSystem.git
cd MGContecnica.FinanceSystem

2. Backend (.NET)
# Restaurar dependências
dotnet restore

# Compilar projeto
dotnet build

# Executar API
dotnet run --project src/MGContecnica.API
API disponível em: http://localhost:5261
Swagger: http://localhost:5261/swagger

3. Frontend (Angular)
# Navegar para projeto Angular
cd MGContecnica.WebApp

# Instalar dependências
npm install

# Executar frontend
npm start
Frontend disponível em: http://localhost:4200

📊 Testando o Sistema
1. Via Swagger (Backend)

Acesse http://localhost:5261/swagger
Teste os endpoints na seguinte ordem:

POST /api/categorias (criar categorias)
POST /api/transacoes (criar transações)
GET /api/relatorios/resumo (relatórios)



2. Via Dashboard (Frontend)

Acesse http://localhost:4200
Visualize o dashboard com resumo financeiro
Veja transações e categorias listadas automaticamente

📚 Endpoints da API
# Transações
GET    /api/transacoes              # Listar (com filtros)
GET    /api/transacoes/{id}         # Buscar por ID
POST   /api/transacoes              # Criar
PUT    /api/transacoes/{id}         # Atualizar
DELETE /api/transacoes/{id}         # Excluir
GET    /api/transacoes/paged        # Listar paginado

# Categorias
GET    /api/categorias              # Listar ativas
POST   /api/categorias              # Criar

# Relatórios
GET    /api/relatorios/resumo       # Resumo financeiro
GET    /api/relatorios/por-categoria # Por categoria

🧪 Executar Testes
# Testes unitários
dotnet test

# Ver cobertura
dotnet test --collect:"XPlat Code Coverage"

📁 Estrutura do Banco
Tabela Categorias

Id (PK, int)
Nome (string, required)
Tipo (int: 1=Receita, 2=Despesa)
Ativo (boolean)
DataCriacao (datetime)

Tabela Transacoes

Id (PK, int)
Descricao (string, required)
Valor (decimal 18,2)
Data (datetime)
CategoriaId (FK, int)
Observacoes (string, nullable)
DataCriacao (datetime)

📝 Logs e Monitoramento

Logs estruturados salvos em src/MGContecnica.API/logs/
Rotação diária com retenção de 30 dias
Níveis: Information, Warning, Error
Console + Arquivo para desenvolvimento

🚀 Próximas Melhorias

 Autenticação e autorização
 Export para Excel/PDF
 Gráficos interativos
 Notificações por email
 Deploy com Docker
 CI/CD pipeline

👨‍💻 Desenvolvedor
Leandro Rocha
📄 Licença
Este projeto está sob a licença MIT.

🏆 Diferenciais Implementados

Arquitetura profissional com separação de responsabilidades
Frontend moderno integrado com backend
Logging avançado para monitoramento
Testes automatizados para qualidade
Performance otimizada com paginação
Documentação completa para manutenção
Design responsivo para múltiplos dispositivos
