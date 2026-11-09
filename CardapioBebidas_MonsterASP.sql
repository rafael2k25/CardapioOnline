-- Script adaptado para o banco já criado na MonsterASP.NET.
-- Execute conectado ao servidor remoto com BancoCardapioDatabase selecionado.
USE [BancoCardapioDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Descricao] [varchar](255) NULL,
	[Ativa] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 09/11/2026 14:37:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[Nome] [varchar](150) NOT NULL,
	[Descricao] [varchar](500) NULL,
	[Preco] [decimal](10, 2) NOT NULL,
	[ImagemUrl] [varchar](500) NULL,
	[Disponivel] [bit] NOT NULL,
	[Ativo] [bit] NOT NULL,
	[DataCadastro] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Produtos_CategoriaId]    Script Date: 09/11/2026 14:37:24 ******/
CREATE NONCLUSTERED INDEX [IX_Produtos_CategoriaId] ON [dbo].[Produtos]
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categorias] ADD  DEFAULT ((1)) FOR [Ativa]
GO
ALTER TABLE [dbo].[Produtos] ADD  DEFAULT ((1)) FOR [Disponivel]
GO
ALTER TABLE [dbo].[Produtos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Produtos] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Categorias] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Categorias]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [CK_Produtos_Preco] CHECK  (([Preco]>=(0)))
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [CK_Produtos_Preco]
GO
