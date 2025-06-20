# ToDoApi

Este projeto implementa uma API RESTful para gestão de tarefas (ToDo) utilizando .NET Core 8, seguindo os princípios de DDD (Domain-Driven Design).

## Estrutura do Projeto

O projeto é dividido nas seguintes camadas:

- **ToDoApi.Api**: Camada de apresentação, contém os controladores da API e configurações de inicialização.
- **ToDoApi.Application**: Camada de aplicação, contém os DTOs, interfaces de serviço e a lógica de negócio de alto nível.
- **ToDoApi.Domain**: Camada de domínio, contém as entidades, enums e interfaces de repositório.
- **ToDoApi.Infrastructure**: Camada de infraestrutura, contém a implementação dos repositórios, o DbContext e a configuração do Entity Framework.
- **ToDoApi.UnitTests**: Projeto de testes unitários para a camada de aplicação.

## Requisitos

- .NET SDK 8.0
- Docker (opcional, para deploy)

## Como Rodar a Aplicação

### 1. Configuração do Banco de Dados

O projeto utiliza Entity Framework Core com um banco de dados SQL Server local (localdb). A string de conexão está configurada no `appsettings.json` do projeto `ToDoApi.Api`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ToDoApiDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Para criar o banco de dados e as tabelas, você pode executar as migrações do Entity Framework. Navegue até o diretório `src/ToDoApi.Infrastructure` e execute os seguintes comandos:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 2. Executando a API

Navegue até o diretório `src/ToDoApi.Api` e execute a aplicação:

```bash
dotnet run
```

A API estará disponível em `https://localhost:7000` (ou outra porta configurada).

### 3. Acessando o Swagger/OpenAPI

Com a API em execução, você pode acessar a documentação interativa do Swagger no seguinte endereço:

`https://localhost:7000/swagger`

### 4. Executando os Testes Unitários

Navegue até o diretório `tests/ToDoApi.UnitTests` e execute os testes:

```bash
dotnet test
```

### 5. Deploy com Docker (Opcional)

Para construir a imagem Docker da aplicação, navegue até o diretório raiz do projeto (`ToDoApi`) onde o `Dockerfile` está localizado e execute:

```bash
docker build -t todoapi .
```

Para rodar a aplicação em um contêiner Docker:

```bash
docker run -p 8080:80 todoapi
```

A API estará disponível em `http://localhost:8080`.


