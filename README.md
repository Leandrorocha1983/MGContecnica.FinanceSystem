# MG Contecnica - Sistema de Controle Financeiro

Sistema de controle financeiro desenvolvido em .NET 8 com arquitetura limpa, permitindo gestão de receitas e despesas com categorização e relatórios financeiros.

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core** - ORM com SQLite
- **C#** - Linguagem de programação
- **Swagger/OpenAPI** - Documentação da API
- **SQLite** - Banco de dados

## 📋 Funcionalidades

### Transações
- ✅ Cadastro de receitas e despesas
- ✅ Listagem com filtros (período, categoria, tipo)
- ✅ Atualização de transações
- ✅ Exclusão de transações
- ✅ Validações de negócio

### Categorias
- ✅ Cadastro de categorias (Receita/Despesa)
- ✅ Listagem de categorias ativas

### Relatórios
- ✅ Resumo financeiro por período (saldo, receitas, despesas)
- ✅ Relatório agrupado por categoria
- ✅ Filtros por data e categoria

## ⚡ Como Executar

Verificar se o .NET está instalado
dotnet --version
Deve mostrar versão 8.0.x

Restaurar dependências do projeto
dotnet restore

Compilar o projeto
dotnet build

Executar a API
dotnet run --project src/MGContecnica.API

Acessar a aplicação
Após executar, a API estará disponível em:

Swagger UI: http://localhost:5261/swagger
API Base URL: http://localhost:5261/api

Testar a API (Passo a passo)
Criar Categorias

Acesse http://localhost:5261/swagger
Clique em POST /api/categorias
Clique em "Try it out"

Criar Transações
Clique em POST /api/transacoes
Clique em "Try it out"

Testar Relatórios

Clique em GET /api/relatorios/resumo
Preencha os parâmetros:
dataInicio:
dataFim: 

📋 Funcionalidades
Transações

✅ Cadastro de receitas e despesas
✅ Listagem com filtros (período, categoria, tipo)
✅ Atualização de transações
✅ Exclusão de transações
✅ Validações de negócio

Categorias

✅ Cadastro de categorias (Receita/Despesa)
✅ Listagem de categorias ativas

Relatórios

✅ Resumo financeiro por período (saldo, receitas, despesas)
✅ Relatório agrupado por categoria
✅ Filtros por data e categoria

⚖️ Validações de Negócio

Valor: Deve ser maior que zero
Data: Não pode ser futura
Categoria: Deve existir e estar ativa
Descrição: Campo obrigatório

🗄️ Banco de Dados
O projeto usa SQLite que é criado automaticamente na primeira execução.
Local do arquivo: MGContecnica.db na pasta raiz do projeto API.
📄 Licença
Este projeto está sob a licença MIT.
EOF

## Clone o repositório
https://github.com/Leandrorocha1983/MGContecnica.FinanceSystem.git

👨‍💻 Desenvolvedor
Leandro Rocha
