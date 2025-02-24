CREATE DATABASE BNE;
GO
USE BNE;

CREATE TABLE Produto (
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Nome VARCHAR (50),
    Preco decimal(10,2)
);

CREATE TABLE Usuario (
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Email VARCHAR(120) NOT NULL,
    Senha VARCHAR(32) NOT NULL,
    Perfil int NOT NULL
);

CREATE TABLE Pedido (
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    EmailComprador VARCHAR(120) NOT NULL,
    Valor decimal(10,2) NOT NULL
);

CREATE TABLE ProdutoPedido (
    ProdutoId UNIQUEIDENTIFIER NOT NULL,
    PedidoId UNIQUEIDENTIFIER NOT NULL,
    Quantidade int NOT NULL,
    PrecoUnitario decimal(10,2),
    PrecoTotal decimal(10,2),
    FOREIGN KEY (ProdutoId) REFERENCES Produto (Id),
    FOREIGN KEY (PedidoId) REFERENCES Pedido (Id),
    PRIMARY KEY (ProdutoId, PedidoId)
);


INSERT INTO Usuario (Email, Senha, Perfil)
VALUES ('samuel.t.almeida@gmail.com', '3A2060058547BCAE595561E3640E6737', 1);