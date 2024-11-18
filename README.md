# Application with DDD in .NET C#
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

1. **Domain**
- ***Tipo de Projeto:*** Biblioteca de Classes

- ***Responsabilidade:*** Contém as regras de negócio fundamentais e a lógica central do sistema, incluindo entidades, objetos de valor, agregados e serviços de domínio.

2. **Shared**

- ***Tipo de Projeto:*** Biblioteca de Classes

- ***Responsabilidade:*** Contém componentes reutilizáveis em toda a aplicação, como DTOs, eventos de domínio, exceções comuns, etc.

3. **Infra (Infrastructure) e Application**

<<<<<<< HEAD
- **Infrastructure:**
=======
- **Infrastructure:** 
>>>>>>> origin/main
    - ***Tipo de Projeto:*** Biblioteca de Classes

    - ***Responsabilidade:*** Implementa componentes de infraestrutura, como persistência de dados (contextos de banco de dados, repositórios concretos), serviços externos e outras dependências que suportam a aplicação.

<<<<<<< HEAD
- **Application:**
=======
- **Application:** 
>>>>>>> origin/main

    - ***Tipo de Projeto:*** Biblioteca de Classes

    - ***Responsabilidade:*** Orquestra a execução das operações de negócio, utilizando comandos e consultas (em uma abordagem CQRS). Esta camada coordena a comunicação entre a camada de domínio e outras camadas.

4. **API**

- ***Tipo de Projeto:*** WebAPI

- ***Responsabilidade:*** Expor endpoints HTTP para clientes externos, como aplicações front-end ou outros serviços. Inclui controladores que recebem requisições HTTP e orquestram a execução dos comandos e consultas.


### RF(Requisitos funcionais)

- [ ] Deve ser possível o usuário realizar cadastro;
- [ ] Deve ser possível o usuário se autenticar;
- [ ] Deve ser possível obter o perfil de um usuário logado;
- [ ] Deve ser possível obter o número de check-in feitos pelo cliente logado;
- [ ] Deve ser possível o cliente buscar academias proximas;
- [ ] Deve ser possível o usuário buscar cliente pelo nome;
- [ ] Deve ser possível o cliente realizar check-in em uma academia;
- [ ] Deve ser possível validar o check-in de um cliente;
- [ ] Deve ser possível cadastrar uma academia;
- [ ] Deve ser possível o cliente obter seu histórico de check-ins
- [ ] Deve ser possível obter o número de check-ins feitos pelo cliente logado


### RN(Regras de negócios)

- [ ] O usuário não deve se cadastrar com um e-mail duplicado;
- [ ] O usuário não pode fazer 2 check-ins no mesmo dia;
- [ ] O usuário não deve fazer check-in se não estiver perto (100 metros) da academia;
- [ ] O check-in só pode ser validado até 20 minutos após ter sido criado;
- [ ] O check-in só pode ser validado por administradores;
- [ ] A academia só poder cadastrada por administradores;

### RNF(Requisitos não-funcionais)

- [ ] A senha do usuário precisa ser criptografada
- [ ] Os dados da aplicação precisam estar persistidos em bacno Postgres SQL;
- [ ] Todas listas de dados precisam estar paginados com 20 items por página;
- [ ] O usuário deve ser indentificado por token JWT(JSON Web Token);