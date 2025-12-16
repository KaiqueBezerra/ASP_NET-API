# CadastroProdutos

✅ **Descrição**

`CadastroProdutos` é uma API RESTful construída com .NET (net10.0) para gerenciamento simples de produtos e usuários. A aplicação usa PostgreSQL como banco de dados e autenticação via JWT. Possui endpoints para CRUD de produtos e gestão de usuários, com regras de autorização (roles `admin` e `user`).

---

## 🔧 Recursos principais

- Autenticação com JWT (`/api/login`) 🛡️
- CRUD de produtos (`/api/products`) — algumas operações exigem role `admin` 🔒
- CRUD de usuários (`/api/users`) — registro público e operações administrativas 🔧
- Documentação Swagger (em ambiente de desenvolvimento) 🧾
- Persistência com PostgreSQL (compatível com Docker) 🐘

---

## ⚙️ Requisitos

- .NET 10 SDK ou superior
- PostgreSQL (local ou via Docker)
- [dotnet-ef] (para rodar migrações, opcional se já tiver migrations aplicadas)

---

## 🚀 Execução local

1. Configure variáveis de ambiente (ou crie um arquivo `.env` na raiz):

```env
CONNECTION_STRING="Host=localhost;Port=5432;Database=asp_net_db;Username=docker;Password=docker"
```

> Observação: o projeto usa `DotNetEnv` para carregar variáveis de ambiente. `Program.cs` lê a variável `CONNECTION_STRING` para configurar o `DbContext`.

2. Suba o banco (opcional via Docker):

```bash
docker compose up -d
```

3. No diretório raiz do projeto, execute:

```bash
dotnet build
dotnet run --project CadastroProdutos
```

A API será iniciada em `https://localhost:5001` (ou porta configurada). Em ambiente `Development` o Swagger UI fica disponível em `/swagger`.

---

## 🗄️ Migrações e banco de dados

- Se precisar aplicar migrações:

```bash
dotnet tool install --global dotnet-ef # (se necessário)
dotnet ef database update --project CadastroProdutos
```

- As migrations já existentes estão no diretório `CadastroProdutos/Migrations/`.

---

## 🔐 Autenticação e autorização

- Endpoint de login: `POST /api/login` com body:

```json
{
  "email": "usuario@exemplo.com",
  "password": "senha"
}
```

- Resposta de sucesso:

```json
{ "token": "<JWT_TOKEN>" }
```

- Para chamadas autenticadas, inclua no header:

```
Authorization: Bearer <JWT_TOKEN>
```

---

## 🧩 Estrutura do projeto

- `Controllers/` — endpoints HTTP
- `Services/` — lógica de negócio
- `Database/` — `ApplicationDbContext` e migrações
- `Models/`, `DTOs/` — entidades e objetos de transferência

---

*Gerado automaticamente com base no código do projeto.*
