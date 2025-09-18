# MediatorBehaviorsFluentValidation

Um projeto ASP.NET Core que demonstra a implementação de **MediatR**, **Pipeline Behaviors** e **FluentValidation** seguindo princípios SOLID e padrões de arquitetura limpa.

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas com separação clara de responsabilidades:

```
MediatorBehaviorsFluentValidation/
├── MediatorBehaviorsFluentValidation/          # API Layer (Controllers, Middleware)
├── MediatorBehaviorsFluentValidation.Domain/   # Domain Layer (Entities, Handlers, Validators)
└── MediatorBehaviorsFluentValidation.Repository/ # Repository Layer (Data Access)
```

## 🛠️ Tecnologias Utilizadas

### Core Framework
- **.NET 9.0** - Framework principal
- **ASP.NET Core** - Web API framework
- **C# 12** - Linguagem de programação

### Principais Bibliotecas
- **MediatR 13.0.0** - Mediator pattern para CQRS
- **FluentValidation 12.0.0** - Validação fluente e expressiva
- **Swashbuckle.AspNetCore 9.0.4** - Documentação Swagger/OpenAPI
- **Microsoft.Extensions.Logging** - Sistema de logging

### Funcionalidades
- **Pipeline Behaviors** - Interceptação de requests para validação
- **Problem Details (RFC 7807)** - Padrão para respostas de erro
- **Static Mappers** - Mapeamento de objetos sem AutoMapper
- **Dependency Injection** - Inversão de controle

## 🎯 Padrões de Projeto Implementados

### 1. **Mediator Pattern**
- **MediatR** implementa o padrão Mediator
- Desacopla comunicação entre objetos
- Centraliza a lógica de comunicação

```csharp
public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
{
    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Lógica de negócio
    }
}
```

### 2. **Pipeline Behavior Pattern**
- **ValidationBehavior** intercepta requests antes do handler
- Executa validações automaticamente
- Implementa cross-cutting concerns

```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validação antes de executar o handler
        return await next();
    }
}
```

### 3. **Repository Pattern**
- **ICustomerRepository** abstrai acesso a dados
- **CustomerRepository** implementa a interface
- Facilita testes e mudanças de persistência

```csharp
public interface ICustomerRepository
{
    Task<int> Create(Customer customer);
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByAsync(int id);
}
```

### 4. **Command Query Responsibility Segregation (CQRS)**
- **Commands** para operações de escrita
- **Queries** para operações de leitura
- **Handlers** específicos para cada operação

### 5. **Static Mapper Pattern**
- **CustomerMapper** com métodos de extensão
- Mapeamento estático para melhor performance
- Substitui AutoMapper por simplicidade

```csharp
public static class CustomerMapper
{
    public static CustomerResponse ToResponse(this Customer customer)
    {
        return new CustomerResponse
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            // ...
        };
    }
}
```

## 📋 Princípios SOLID Aplicados

### **S** - Single Responsibility Principle (SRP)
- ✅ **Handlers** têm responsabilidade única (uma operação específica)
- ✅ **Validators** são responsáveis apenas pela validação
- ✅ **Repository** gerencia apenas acesso a dados
- ✅ **Controllers** apenas coordenam requests

### **O** - Open/Closed Principle (OCP)
- ✅ **Pipeline Behaviors** podem ser estendidos sem modificar código existente
- ✅ **Validators** podem ser adicionados sem alterar handlers
- ✅ **Mappers** podem ser estendidos com novos métodos

### **L** - Liskov Substitution Principle (LSP)
- ✅ **ICustomerRepository** pode ser substituído por qualquer implementação
- ✅ **IRequestHandler** permite substituição de handlers
- ✅ **IValidator** permite diferentes implementações de validação

### **I** - Interface Segregation Principle (ISP)
- ✅ **ICustomerRepository** contém apenas métodos necessários
- ✅ **IRequestHandler** é específico para cada tipo de request
- ✅ Interfaces pequenas e focadas

### **D** - Dependency Inversion Principle (DIP)
- ✅ **Handlers** dependem de abstrações (ICustomerRepository)
- ✅ **Controllers** dependem de abstrações (IMediator)
- ✅ **ValidationBehavior** depende de abstrações (IValidator)

## 🚀 Funcionalidades Principais

### 1. **Validação Automática**
- Validação fluente com FluentValidation
- Interceptação automática via Pipeline Behavior
- Retorno de erros estruturados

### 2. **Tratamento de Erros**
- **Erros de Validação (400)**: Lista simples com código e mensagem
- **Erros 404**: Problem Details para recursos não encontrados
- **Erros 500**: Problem Details com informações de trace

### 3. **Documentação API**
- Swagger/OpenAPI integrado
- Documentação automática dos endpoints
- Exemplos de requests e responses

### 4. **Performance**
- Mappers estáticos (sem reflection)
- Pipeline behaviors otimizados
- Dependency injection eficiente

## 📁 Estrutura de Pastas

```
MediatorBehaviorsFluentValidation.Domain/
├── Commands/                    # Commands para operações de escrita
├── Queries/                     # Queries para operações de leitura
├── Handlers/                    # Handlers para Commands e Queries
├── Entity/                      # Entidades de domínio
├── Interfaces/                  # Contratos/Interfaces
├── Mappings/                    # Mappers estáticos
├── PipeLineBehaviors/           # Behaviors para interceptação
├── Responses/                   # DTOs de resposta
└── Validation/                  # Validators do FluentValidation

MediatorBehaviorsFluentValidation/
├── Controllers/                 # Controllers da API
├── Extensions/                  # Extensões customizadas
└── Program.cs                   # Configuração da aplicação

MediatorBehaviorsFluentValidation.Repository/
└── Repository/                  # Implementações de repositório
```

## 🔧 Como Executar

### Pré-requisitos
- .NET 9.0 SDK
- Visual Studio 2022 ou VS Code

### Passos
1. Clone o repositório
2. Restaure as dependências:
   ```bash
   dotnet restore
   ```
3. Execute o projeto:
   ```bash
   dotnet run --project MediatorBehaviorsFluentValidation
   ```
4. Acesse a documentação Swagger em: `https://localhost:7000/swagger`

## 📊 Endpoints Disponíveis

### Customers
- `GET /api/customers` - Lista todos os clientes
- `GET /api/customers/{id}` - Busca cliente por ID
- `POST /api/customers` - Cria novo cliente

### Exemplo de Request
```json
POST /api/customers
{
  "firstName": "João",
  "lastName": "Silva",
  "email": "joao.silva@email.com"
}
```

### Exemplo de Response (Erro de Validação)
```json
{
  "errors": [
    {
      "code": "NotEmptyValidator",
      "message": "'First Name' must not be empty."
    },
    {
      "code": "EmailValidator",
      "message": "'Email' is not a valid email address."
    }
  ]
}
```

## 🧪 Testes Unitários

O projeto possui uma **suíte completa de testes unitários** implementada com **XUnit**, garantindo alta qualidade e confiabilidade do código.

### 📊 Resumo dos Testes
- **64 testes implementados** com 100% de sucesso
- **Framework**: XUnit + Moq + FluentAssertions
- **Tempo de execução**: ~1 segundo
- **Cobertura**: Todos os componentes principais testados

### 🏗️ Estrutura do Projeto de Testes
```
MediatorBehaviorsFluentValidation.Tests/
├── Controllers/           # Testes de endpoints (6 testes)
├── Handlers/             # Testes de handlers MediatR (11 testes)
├── Mappings/             # Testes de mappers estáticos (12 testes)
├── PipelineBehaviors/    # Testes de behaviors (5 testes)
└── Validation/           # Testes de validadores (12 testes)
```

### 🎯 Componentes Testados

#### **Handlers** (11 testes)
- **CreateCustomerHandler**: Validação de criação de clientes
- **GetAllCustomerHandler**: Listagem de clientes
- **GetByIdCustomerHandler**: Busca por ID

**Cenários testados:**
- ✅ Comandos válidos e inválidos
- ✅ Retorno de dados corretos
- ✅ Propagação de exceções
- ✅ Valores nulos e casos extremos

#### **Validators** (12 testes)
- **CreateCustomerCommandValidator**: Validação fluente completa

**Cenários testados:**
- ✅ Campos obrigatórios (FirstName, LastName, Email)
- ✅ Formato de email válido
- ✅ Tamanho mínimo de campos
- ✅ Valores nulos e strings vazias
- ✅ Múltiplos erros simultâneos

#### **Pipeline Behaviors** (5 testes)
- **ValidationBehavior**: Interceptação de requests

**Cenários testados:**
- ✅ Validação bem-sucedida
- ✅ Validação com falhas
- ✅ Múltiplos validadores
- ✅ Ausência de validadores
- ✅ Propagação de exceções

#### **Mappers** (12 testes)
- **CustomerMapper**: Mapeamento estático

**Cenários testados:**
- ✅ Conversão Customer → CustomerResponse
- ✅ Conversão CreateCustomerCommand → Customer
- ✅ Conversão Customer → CreateCustomerCommand
- ✅ Valores nulos e listas vazias
- ✅ Múltiplos objetos

#### **Controllers** (6 testes)
- **CustomersController**: Endpoints da API

**Cenários testados:**
- ✅ Endpoints GET e POST
- ✅ Retorno de dados corretos
- ✅ Propagação de exceções
- ✅ Validação de comandos

### 🚀 Como Executar os Testes

#### Executar Todos os Testes
```bash
dotnet test
```

#### Executar com Cobertura de Código
```bash
dotnet test --collect:"XPlat Code Coverage"
```

#### Executar com Log Detalhado
```bash
dotnet test --logger "console;verbosity=normal"
```

#### Executar Testes Específicos
```bash
# Apenas testes de Handlers
dotnet test --filter "Handlers"

# Apenas testes de Validação
dotnet test --filter "Validation"

# Apenas testes de Mappers
dotnet test --filter "Mappings"
```

### 🛠️ Tecnologias de Teste Utilizadas

- **XUnit** - Framework principal de testes
- **Moq** - Mocking de dependências para isolamento
- **FluentAssertions** - Assertions mais expressivas
- **coverlet.collector** - Coleta de métricas de cobertura
- **Microsoft.AspNetCore.Mvc.Testing** - Testes de integração

### 📋 Padrões de Teste Implementados

#### **Arrange-Act-Assert (AAA)**
Todos os testes seguem o padrão AAA para clareza:
```csharp
[Fact]
public async Task Handle_ValidCommand_ShouldReturnCustomerId()
{
    // Arrange
    var command = new CreateCustomerCommand { /* ... */ };
    _repositoryMock.Setup(/* ... */);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.Equal(expectedId, result);
}
```

#### **Mocking de Dependências**
Uso extensivo do Moq para isolar unidades sob teste:
```csharp
private readonly Mock<ICustomerRepository> _repositoryMock;
private readonly Mock<ILogger<CreateCustomerHandler>> _loggerMock;
```

#### **Testes Parametrizados**
Uso de `[Theory]` e `[InlineData]` para testar múltiplos cenários:
```csharp
[Theory]
[InlineData("")]
[InlineData(" ")]
[InlineData("string")]
public void Validate_InvalidFirstName_ShouldFail(string firstName)
{
    // Teste para múltiplos valores
}
```

### 🎯 Qualidade dos Testes

- ✅ **Cobertura Completa**: Todos os componentes principais testados
- ✅ **Isolamento**: Uso correto de mocks para dependências
- ✅ **Cenários Diversos**: Casos válidos, inválidos e extremos
- ✅ **Nomenclatura Clara**: Nomes descritivos dos métodos de teste
- ✅ **Organização**: Estrutura de pastas bem definida

### 🔧 Facilidades para Testes

O projeto está estruturado para facilitar testes unitários:
- **Handlers** podem ser testados isoladamente com mocks
- **Validators** são testáveis independentemente
- **Repository** pode ser mockado facilmente
- **Pipeline Behaviors** podem ser testados separadamente
- **Dependency Injection** facilita a substituição de dependências
- **Interfaces bem definidas** permitem mocking eficiente

## 🔄 Melhorias Implementadas

### Performance
- ✅ Removido AutoMapper em favor de mappers estáticos
- ✅ Pipeline behaviors otimizados
- ✅ Dependency injection eficiente

### Tratamento de Erros
- ✅ Lista simples para erros de validação
- ✅ Problem Details para erros críticos
- ✅ Middleware customizado para FluentValidation

### Arquitetura
- ✅ Separação clara de responsabilidades
- ✅ Princípios SOLID aplicados
- ✅ Padrões de projeto bem implementados

## 📚 Referências

- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [FluentValidation Documentation](https://docs.fluentvalidation.net/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [RFC 7807 - Problem Details](https://tools.ietf.org/html/rfc7807)

## 👥 Contribuição

Este projeto serve como exemplo de implementação de padrões de arquitetura limpa e princípios SOLID em .NET. Sinta-se à vontade para usar como referência em seus projetos.

---

**Desenvolvido usando .NET 9.0**


- **Desenvolvedor**: [Kássio Fernandes Lima]
- **Email**: [kassioflima@gmail.com]