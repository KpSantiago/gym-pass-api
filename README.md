# Gym-Pass API 2.0
API para a gestão de check-ins em academias, feita em .NET 8 é uma recriação de uma antiga API feita em Node.js.
Nela é possível realizar o gerenciamento de academias, check-ins e informações de usuário, abordando conceitos, como
CQRS, RBAC, DDD e SOLID principles.

#### Tecnologias usadas

- .NET 8
- ASP.NET
- xUnit (Unit Testing)
- Entity Framework Core
- Swagger (Documentação de API)

#### Conceitos usados

- Domain-Driven Design (DDD)
- SOLID principles
- Role-Based Access Controll (RBAC)
- Command Query Responsability Segregation (CQRS)


`User`

| Method | Path | Action |
| ------ | ---- | ------ |
| POST   | /api/v1/register | Realiza o cadastro de um novo usuário |
| POST   | /api/v1/session  | Realiza a autenticação do usuário |
| GET    | /api/v1/user/profile | Resgata as informações do usuário |

`Gyms`

| Method | Path | Action |
| ------ | ---- | ------ |
| POST   | /api/v1/gyms | Realiza o cadastro de uma nova academia (Apenas admin) |
| GET    | /api/v1/nearby  | Busca as academias próxmias com base na localização do usuário |
| GET    | /api/v1/dearch | Bucsa por academias |

`Check-Ins`

| Method | Path | Action |
| ------ | ---- | ------ |
| POST   | /api/v1/check-ins | Realiza o cadastro de um novo check-in |
| GET    | /api/v1/check-ins/user/:userId/history  | Resgata o histórico de check-ins do usuário |
| GET    | /api/v1/check-ins/user/:userId/metrics | Resgata as métricas de check-ins do usuário |
| PATCH  | /api/v1/check-ins/:checkInId/validate | Faz a validação de um check-in ao chegar na academia |


### Application with DDD in .NET C#
```plaintext
/src
  /Domain
    - Biblioteca de Classes
    - Entidades, Agregados, Repositórios (Interfaces), Serviços de Domínio
  /Application
    - Biblioteca de Classes
    - Comandos, Consultas, Manipuladores
  /Infrastructure
    - Biblioteca de Classes
    - Persistência, Repositórios (Implementações), Serviços Externos
  /Shared
    - Biblioteca de Classes
    - DTOs, Eventos, Exceções Comuns
  /API
    - WebAPI
    - Controladores, Configuração de Roteamento
```

### RF(Requisitos funcionais)

- [x] Deve ser possível o usuário realizar cadastro;
- [x] Deve ser possível o usuário se autenticar;
- [x] Deve ser possível obter o perfil de um usuário logado;
- [x] Deve ser possível obter o número de check-in feitos pelo cliente logado;
- [x] Deve ser possível o cliente buscar academias proximas;
- [x] Deve ser possível o usuário buscar cliente pelo nome;
- [x] Deve ser possível o cliente realizar check-in em uma academia;
- [x] Deve ser possível validar o check-in de um cliente;
- [x] Deve ser possível cadastrar uma academia;
- [x] Deve ser possível o cliente obter seu histórico de check-ins
- [x] Deve ser possível obter o número de check-ins feitos pelo cliente logado


### RN(Regras de negócios)

- [x] O usuário não deve se cadastrar com um e-mail duplicado;
- [x] O usuário não pode fazer 2 check-ins no mesmo dia;
- [x] O usuário não deve fazer check-in se não estiver perto (100 metros) da academia;
- [x] O check-in só pode ser validado até 20 minutos após ter sido criado;
- [x] O check-in só pode ser validado por administradores;
- [x] A academia só poder cadastrada por administradores;

### RNF(Requisitos não-funcionais)

- [x] A senha do usuário precisa ser criptografada
- [x] Os dados da aplicação precisam estar persistidos em bacno Postgres SQL;
- [x] Todas listas de dados precisam estar paginados com 20 items por página;
- [x] O usuário deve ser indentificado por token JWT(JSON Web Token);