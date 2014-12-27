
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/27/2014 13:12:09
-- Generated from EDMX file: E:\Teste Inter S\AppWebService\AppWebServiceTest\AppWebServiceTest\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PessoaUtilizador]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UtilizadorSet] DROP CONSTRAINT [FK_PessoaUtilizador];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PessoaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet];
GO
IF OBJECT_ID(N'[dbo].[UtilizadorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UtilizadorSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PessoaSet'
CREATE TABLE [dbo].[PessoaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [BI] int  NOT NULL,
    [Idade] int  NOT NULL
);
GO

-- Creating table 'UtilizadorSet'
CREATE TABLE [dbo].[UtilizadorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [isAdmin] bit  NOT NULL,
    [Pessoa_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PessoaSet'
ALTER TABLE [dbo].[PessoaSet]
ADD CONSTRAINT [PK_PessoaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UtilizadorSet'
ALTER TABLE [dbo].[UtilizadorSet]
ADD CONSTRAINT [PK_UtilizadorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Pessoa_Id] in table 'UtilizadorSet'
ALTER TABLE [dbo].[UtilizadorSet]
ADD CONSTRAINT [FK_PessoaUtilizador]
    FOREIGN KEY ([Pessoa_Id])
    REFERENCES [dbo].[PessoaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PessoaUtilizador'
CREATE INDEX [IX_FK_PessoaUtilizador]
ON [dbo].[UtilizadorSet]
    ([Pessoa_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------