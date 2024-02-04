Projeto em C# 8.0 com Mediator, Fluent Validation e AutoMapper

1. Camada de API
* Injeção de Dependência: Configuração e registro de dependências, incluindo o Mediator.
2. Camada de Aplicação(Application Layer):
* DTOs (Data Transfer Objects): Objetos utilizados para transferir dados entre as
* Handlers: Manipuladores de comandos e consultas que contêm a lógica de negócios.
* Queries e Commands: Classes que representam solicitações e comandos do sistema.
* Mediator: Implementação do padrão Mediator para orquestrar a execução de comandos e consultas.
* Entidades: Classes que representam os conceitos principais do domínio.
* Validações de Domínio: Utilização do Fluent Validation para garantir que as entidades estejam em um estado válido.
* Automapper Profiles: Configurações do AutoMapper para mapear entidades para DTOs e vice-versa.
3. Camada de Infraestrutura (Infrastructure Layer):

Repositórios: Implementações concretas de interfaces de repositório para acessar dados.

Tecnologias Utilizadas:

1. C# 8.0: A linguagem principal do projeto.
2. Mediator Pattern: Utilizado para desacoplar a lógica de negócios em comandos e consultas, facilitando a manutenção e testabilidade.
3. Fluent Validation: Usado para definir regras de validação de domínio de forma expressiva e concisa.
4. AutoMapper: Facilita a transferência de dados entre objetos de diferentes tipos, como entidades e DTOs.

Benefícios:

* Desacoplamento de componentes, facilitando a manutenção e a evolução do código.
* Clareza na validação de dados usando Fluent Validation.
* Transferência eficiente de dados entre diferentes camadas com AutoMapper.

Esta é uma visão geral geral, e a implementação específica pode variar dependendo dos requisitos do projeto e das preferências da equipe de desenvolvimento.
