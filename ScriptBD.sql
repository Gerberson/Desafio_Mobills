USE [ContasAPagar]
GO
/****** Object:  Table [dbo].[Despesas]    Script Date: 25/08/2020 15:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Despesas](
	[IdDespesa] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Valor] [money] NOT NULL,
	[Data] [datetime] NOT NULL,
	[Pago] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receitas]    Script Date: 25/08/2020 15:30:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receitas](
	[IdReceita] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Valor] [money] NOT NULL,
	[Data] [datetime] NOT NULL,
	[Recebido] [bit] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Despesas] ON 

INSERT [dbo].[Despesas] ([IdDespesa], [Descricao], [Valor], [Data], [Pago]) VALUES (1, N'Luz', 180.5200, CAST(N'2020-05-15T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Despesas] ([IdDespesa], [Descricao], [Valor], [Data], [Pago]) VALUES (2, N'Carro', 922.3200, CAST(N'2020-05-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Despesas] ([IdDespesa], [Descricao], [Valor], [Data], [Pago]) VALUES (3, N'Aluguel', 890.0000, CAST(N'2020-05-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Despesas] ([IdDespesa], [Descricao], [Valor], [Data], [Pago]) VALUES (4, N'Condominio', 560.0000, CAST(N'2020-05-15T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Despesas] OFF
GO
SET IDENTITY_INSERT [dbo].[Receitas] ON 

INSERT [dbo].[Receitas] ([IdReceita], [Descricao], [Valor], [Data], [Recebido]) VALUES (1, N'Salario', 2500.5000, CAST(N'2020-05-05T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Receitas] ([IdReceita], [Descricao], [Valor], [Data], [Recebido]) VALUES (2, N'Investimento', 1000.6500, CAST(N'2020-05-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Receitas] ([IdReceita], [Descricao], [Valor], [Data], [Recebido]) VALUES (3, N'Sistema', 800.6300, CAST(N'2020-05-22T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Receitas] ([IdReceita], [Descricao], [Valor], [Data], [Recebido]) VALUES (4, N'Mercadinho', 526.8500, CAST(N'2020-05-15T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Receitas] OFF
GO
