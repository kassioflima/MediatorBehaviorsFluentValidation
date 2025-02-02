# MediatorBehaviorsFluentValidation

Este projeto é um exemplo prático que demonstra a integração do padrão Mediator com FluentValidation, utilizando *behaviors* para criar um pipeline de processamento robusto e modular. O objetivo é facilitar a implementação de regras de validação e outros comportamentos transversais (cross-cutting concerns) de forma organizada e escalável em aplicações .NET.

---

## Tecnologias Utilizadas

- **.NET Core / .NET 5+**  
  Plataforma para desenvolvimento de aplicações modernas e de alta performance.

- **MediatR**  
  Biblioteca que implementa o padrão Mediator, promovendo a separação de responsabilidades e reduzindo o acoplamento entre os componentes da aplicação.

- **FluentValidation**  
  Biblioteca para criação de regras de validação de forma fluida e expressiva, facilitando a manutenção e a legibilidade do código.

- **Behaviors no Pipeline do MediatR**  
  Mecanismo que permite a execução de lógica adicional (como validações, logging, etc.) antes ou depois da execução dos *handlers* dos comandos e consultas.

---

## Benefícios

- **Organização e Manutenção:**  
  Com o uso do Mediator e dos behaviors, as regras de negócio e as validações ficam desacopladas dos *handlers*, facilitando a manutenção e a evolução do código.

- **Reusabilidade:**  
  As validações implementadas com FluentValidation podem ser facilmente reutilizadas em diferentes partes da aplicação, promovendo um código mais limpo e DRY (Don't Repeat Yourself).

- **Escalabilidade:**  
  A implementação modular permite adicionar novos comportamentos no pipeline sem a necessidade de alterar os *handlers*, facilitando a escalabilidade e a integração de novos recursos.

- **Testabilidade:**  
  A separação de responsabilidades torna os componentes mais fáceis de testar de forma isolada, garantindo maior qualidade e confiabilidade do sistema.

---

## Estrutura do Projeto

A organização dos arquivos e pastas segue boas práticas para facilitar a navegação e a manutenção:

- **/src**  
  Contém o código fonte principal do projeto, incluindo:
  - **Handlers:** Classes responsáveis por processar comandos e consultas.
  - **Validators:** Regras de validação implementadas com FluentValidation.
  - **Behaviors:** Implementações de *pipeline behaviors* que encapsulam lógicas transversais, como validação e logging.

- **/tests**  
  (Opcional) Projetos ou pastas dedicadas a testes unitários e de integração, garantindo a qualidade do código.

- **Arquivos de Configuração:**  
  Arquivos como `appsettings.json` (para configurações de ambiente) e outros que auxiliam na configuração do pipeline e dos serviços.

---

## Instalação e Execução

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/kassioflima/MediatorBehaviorsFluentValidation.git
