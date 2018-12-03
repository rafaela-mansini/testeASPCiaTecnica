# testeASPCiaTecnica
Teste de Programação ASP.NET MVC

Para alterar as conexões do Banco de dados, basta acessar Models/Conexao

Segue script de banco de dados SQL Server

USE [cia_tecnica]
GO

/****** Object:  Table [dbo].[cliente]    Script Date: 03/12/2018 03:02:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[documento] [varchar](50) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[sobrenome] [varchar](15) NULL,
	[razao_social] [varchar](50) NULL,
	[data_nascimento] [date] NULL,
	[tipo_cliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [cia_tecnica]
GO

/****** Object:  Table [dbo].[endereco]    Script Date: 03/12/2018 03:02:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[endereco](
	[id_endereco] [int] IDENTITY(1,1) NOT NULL,
	[cep] [varchar](8) NULL,
	[logradouro] [varchar](50) NULL,
	[numero] [varchar](50) NULL,
	[bairro] [varchar](50) NULL,
	[cidade] [varchar](50) NULL,
	[uf] [varchar](8) NULL,
	[complemento] [varchar](50) NOT NULL,
	[id_cliente] [int] NULL,
 CONSTRAINT [PK_endereco] PRIMARY KEY CLUSTERED 
(
	[id_endereco] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



