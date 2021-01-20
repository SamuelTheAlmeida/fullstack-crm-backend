### FullStack CRM Back-end

#### Pré-requisitos

- Docker instalado

#### Executar 

`$ docker-compose up -d`

- **O back-end estará disponível acessando** [localhost:8080](localhost:8080)
- **RabbitMQ**: [localhost:15672](localhost:15672)
- **SQL Server:** [localhost:1433](localhost:1433)


- Para autenticar diretamente na API, chamar a rota de login passando o usuário e a  senha (MD5)
- Poderá ser usado o usuário padrão ou criado um novo cadastro

##### Perfis
- Administrador (1): Gerencia Usuários, Produtos e Pedidos
- Funcionário (2): Gerencia Produtos e Pedidos

#### Usuários padrão: 
##### API
usuário: samuel.t.almeida@gmail.com
senha: bne (criptografar em MD5 para a API)
##### RabbitMQ
usuário: rabbitmq
senha: rabbitmq
##### SQL Server
usuário: SA
senha: S3nh@123
