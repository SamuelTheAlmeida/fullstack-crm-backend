CREATE DATABASE BNE;
GO
USE BNE;

CREATE TABLE Produto (
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Nome VARCHAR (50),
    Preco decimal
);

CREATE TABLE Usuario (
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Email VARCHAR(120) NOT NULL,
    Senha VARCHAR(32) NOT NULL,
    Perfil int NOT NULL
);

INSERT INTO Usuario (Email, Senha, Perfil)
VALUES ('samuel.t.almeida@gmail.com', '3A2060058547BCAE595561E3640E6737', 2);