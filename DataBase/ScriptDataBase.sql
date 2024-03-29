USE [ManualDeploymentRazor]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 31/07/2023 2:54:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applicatives]    Script Date: 31/07/2023 2:54:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applicatives](
	[ApplicativeID] [int] IDENTITY(1,1) NOT NULL,
	[NameApplicative] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Applicatives] PRIMARY KEY CLUSTERED 
(
	[ApplicativeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blueprints]    Script Date: 31/07/2023 2:54:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blueprints](
	[BlueprintID] [int] IDENTITY(1,1) NOT NULL,
	[Version] [real] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Route] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
 CONSTRAINT [PK_Blueprints] PRIMARY KEY CLUSTERED 
(
	[BlueprintID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Changes]    Script Date: 31/07/2023 2:54:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Changes](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[State] [bit] NOT NULL,
	[changeDescription] [nvarchar](max) NOT NULL,
	[requestType] [nvarchar](max) NOT NULL,
	[changerNumber] [nvarchar](max) NOT NULL,
	[checkList] [bit] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[creationDate] [datetime2](7) NOT NULL,
	[modificationDate] [datetime2](7) NOT NULL,
	[applicationDate] [datetime2](7) NOT NULL,
	[deploymentDate] [datetime2](7) NOT NULL,
	[changeType] [nvarchar](max) NOT NULL,
	[Rollback] [nvarchar](max) NOT NULL,
	[Version] [int] NOT NULL,
	[IsTemplate] [bit] NOT NULL,
	[EnvironmentID] [int] NOT NULL,
 CONSTRAINT [PK_Changes] PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[Observations] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnvironmentApplicatives]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnvironmentApplicatives](
	[EnvironmentApplicativeID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicativeID] [int] NOT NULL,
	[ServerID] [int] NOT NULL,
	[EnvironmentID] [int] NOT NULL,
 CONSTRAINT [PK_EnvironmentApplicatives] PRIMARY KEY CLUSTERED 
(
	[EnvironmentApplicativeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Environments]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Environments](
	[EnvironmentID] [int] IDENTITY(1,1) NOT NULL,
	[NameEnvironment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Environments] PRIMARY KEY CLUSTERED 
(
	[EnvironmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FunctionalUsers]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FunctionalUsers](
	[FunctionalUserID] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[DataStartTime] [datetime2](7) NOT NULL,
	[DataEndTime] [datetime2](7) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_FunctionalUsers] PRIMARY KEY CLUSTERED 
(
	[FunctionalUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plans]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[PlanID] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[DataStartTime] [datetime2](7) NOT NULL,
	[DataEndTime] [datetime2](7) NOT NULL,
	[ExecutionTime] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Responsible] [nvarchar](max) NOT NULL,
	[supplierArea] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Plans] PRIMARY KEY CLUSTERED 
(
	[PlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Postimplantacions]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Postimplantacions](
	[PostimplantacionID] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[DataStartTime] [datetime2](7) NOT NULL,
	[DataEndTime] [datetime2](7) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Postimplantacions] PRIMARY KEY CLUSTERED 
(
	[PostimplantacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prerrequisitos]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prerrequisitos](
	[PrerrequisitoID] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[DataStart] [datetime2](7) NOT NULL,
	[DataEnd] [datetime2](7) NOT NULL,
	[ExecutionTime] [int] NOT NULL,
	[SupplierArea] [nvarchar](max) NOT NULL,
	[Responsible] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Prerrequisitos] PRIMARY KEY CLUSTERED 
(
	[PrerrequisitoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Results]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results](
	[ResultID] [int] IDENTITY(1,1) NOT NULL,
	[Approved] [nvarchar](max) NOT NULL,
	[Reprobate] [nvarchar](max) NOT NULL,
	[Error] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED 
(
	[ResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rols]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rols](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[TypeoRol] [nvarchar](max) NOT NULL,
	[State] [bit] NOT NULL,
 CONSTRAINT [PK_Rols] PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolUsers]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUsers](
	[RolUserID] [int] IDENTITY(1,1) NOT NULL,
	[State] [bit] NOT NULL,
	[RolID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_RolUsers] PRIMARY KEY CLUSTERED 
(
	[RolUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servers]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servers](
	[ServerID] [int] IDENTITY(1,1) NOT NULL,
	[NameServer] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Servers] PRIMARY KEY CLUSTERED 
(
	[ServerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Signatures]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Signatures](
	[SignatureID] [int] IDENTITY(1,1) NOT NULL,
	[Observatins] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Signatures] PRIMARY KEY CLUSTERED 
(
	[SignatureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainings]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainings](
	[TrainingID] [int] IDENTITY(1,1) NOT NULL,
	[Comments] [nvarchar](max) NOT NULL,
	[DataTraining] [datetime2](7) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Objective] [nvarchar](max) NOT NULL,
	[Issues] [nvarchar](max) NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ChangesChangeID] [int] NULL,
 CONSTRAINT [PK_Trainings] PRIMARY KEY CLUSTERED 
(
	[TrainingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserChanges]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserChanges](
	[UserChangeID] [int] IDENTITY(1,1) NOT NULL,
	[State] [bit] NOT NULL,
	[ChangeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserChanges] PRIMARY KEY CLUSTERED 
(
	[UserChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31/07/2023 2:54:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[NetworkUser] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [int] NOT NULL,
	[Area] [nvarchar](max) NOT NULL,
	[Position] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230526150326_DBV1', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230705131146_V2', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230718163956__V3', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230726203901__V5', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230726205315__V5', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Applicatives] ON 

INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (1, N'DPW')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (2, N'Portal ARL')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (3, N'Portal Asesor')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (1002, N'Prueba')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (1003, N'Prueba')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (1004, N'Prueba')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (1005, N'Portal ARL')
INSERT [dbo].[Applicatives] ([ApplicativeID], [NameApplicative]) VALUES (1006, N'Portal Asesor')
SET IDENTITY_INSERT [dbo].[Applicatives] OFF
GO
SET IDENTITY_INSERT [dbo].[Blueprints] ON 

INSERT [dbo].[Blueprints] ([BlueprintID], [Version], [Date], [Route], [ChangeID]) VALUES (2, 11, CAST(N'2023-07-20T00:00:00.0000000' AS DateTime2), N'https://asesorescolpatria.sharepoint.com/:u:/s/Tecnologia/EVvFp-tVp5FJngwpkH1Ah04BK9rxR-mhQ-EuGheRM9j6rg?e=YeOAzn', 15)
SET IDENTITY_INSERT [dbo].[Blueprints] OFF
GO
SET IDENTITY_INSERT [dbo].[Changes] ON 

INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (15, 1, N'RF-01', N'Requerimiento', N'CHG-01', 1, CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-03-17T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-10T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), N'Normal', N'En caso de falla en el punto 2 devolver al valor ', 1, 1, 1)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (19, 1, N'RF-02', N'Mantenimiento', N'CHG-02', 1, CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), N'Estandar', N'En caso de falla en el punto 2 devolver al valor ', 2, 0, 2)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (1002, 1, N'RF-81142-1-42105- LOG PROCESO DE ENVÍO DE CORREOS DESDE EL GESTOR DE VENTAS DE AUTOS', N'Requerimiento', N'CHG-89402-1-2420', 0, CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-03-17T00:00:00.0000000' AS DateTime2), CAST(N'2019-03-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), N'Estandar', N'En caso de falla en el punto 2 devolver al valor ', 0, 1, 1)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (1003, 0, N'RF-81142-1-42161-', N'Funcional', N'CHG-89402-1-42161', 1, CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), N'2', N'2', 2, 1, 2)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (2004, 1, N'RF-05', N'Funcional', N'CHG-05', 1, CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), N'Estandar', N'En caso de fallo', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Changes] OFF
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([ContactID], [Observations], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1, N'N/A', 15, 3, NULL)
INSERT [dbo].[Contacts] ([ContactID], [Observations], [ChangeID], [UserID], [ChangesChangeID]) VALUES (2, N'N/A', 15, 1007, NULL)
INSERT [dbo].[Contacts] ([ContactID], [Observations], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1001, N'N/A', 1002, 3, NULL)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[EnvironmentApplicatives] ON 

INSERT [dbo].[EnvironmentApplicatives] ([EnvironmentApplicativeID], [ApplicativeID], [ServerID], [EnvironmentID]) VALUES (1, 1, 1, 1)
INSERT [dbo].[EnvironmentApplicatives] ([EnvironmentApplicativeID], [ApplicativeID], [ServerID], [EnvironmentID]) VALUES (2, 2, 2, 1)
INSERT [dbo].[EnvironmentApplicatives] ([EnvironmentApplicativeID], [ApplicativeID], [ServerID], [EnvironmentID]) VALUES (3, 1, 3, 2)
INSERT [dbo].[EnvironmentApplicatives] ([EnvironmentApplicativeID], [ApplicativeID], [ServerID], [EnvironmentID]) VALUES (4, 2, 4, 2)
INSERT [dbo].[EnvironmentApplicatives] ([EnvironmentApplicativeID], [ApplicativeID], [ServerID], [EnvironmentID]) VALUES (7, 1, 5, 3)
INSERT [dbo].[EnvironmentApplicatives] ([EnvironmentApplicativeID], [ApplicativeID], [ServerID], [EnvironmentID]) VALUES (1003, 3, 4, 1)
SET IDENTITY_INSERT [dbo].[EnvironmentApplicatives] OFF
GO
SET IDENTITY_INSERT [dbo].[Environments] ON 

INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (1, N'PDC')
INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (2, N'TST')
INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (3, N'UAT')
INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (1002, N'')
SET IDENTITY_INSERT [dbo].[Environments] OFF
GO
SET IDENTITY_INSERT [dbo].[FunctionalUsers] ON 

INSERT [dbo].[FunctionalUsers] ([FunctionalUserID], [Sequence], [DataStartTime], [DataEndTime], [ChangeID], [UserID], [ChangesChangeID]) VALUES (2, 1, CAST(N'2023-11-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-21T00:00:00.0000000' AS DateTime2), 19, 1002, NULL)
INSERT [dbo].[FunctionalUsers] ([FunctionalUserID], [Sequence], [DataStartTime], [DataEndTime], [ChangeID], [UserID], [ChangesChangeID]) VALUES (3, 2, CAST(N'2023-11-22T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-23T00:00:00.0000000' AS DateTime2), 19, 1004, NULL)
SET IDENTITY_INSERT [dbo].[FunctionalUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[Plans] ON 

INSERT [dbo].[Plans] ([PlanID], [Sequence], [DataStartTime], [DataEndTime], [ExecutionTime], [Description], [Responsible], [supplierArea], [ChangeID], [ChangesChangeID]) VALUES (2, 1, CAST(N'2022-12-04T00:00:00.0000000' AS DateTime2), CAST(N'2022-12-04T00:10:00.0000000' AS DateTime2), 10, N'Generar snapshot de los servidores DC1PVPPOR1, DC1PVPPOR2 , DC2PVPPOR1, dc1pvpdpw1 y dc1pvpdpw2', N'Admin. Servidores', N'Infrastructura', 15, NULL)
INSERT [dbo].[Plans] ([PlanID], [Sequence], [DataStartTime], [DataEndTime], [ExecutionTime], [Description], [Responsible], [supplierArea], [ChangeID], [ChangesChangeID]) VALUES (3, 2, CAST(N'2022-12-04T00:10:00.0000000' AS DateTime2), CAST(N'2022-12-04T00:20:00.0000000' AS DateTime2), 10, N'Desolidificar (deshabilitar el Solidcore) los servidores dc2qvawsv1, dc2qvawsv2			', N'Seguridad TI', N'Infraestructura', 15, NULL)
INSERT [dbo].[Plans] ([PlanID], [Sequence], [DataStartTime], [DataEndTime], [ExecutionTime], [Description], [Responsible], [supplierArea], [ChangeID], [ChangesChangeID]) VALUES (4, 3, CAST(N'2022-12-04T00:20:00.0000000' AS DateTime2), CAST(N'2022-12-04T00:30:00.0000000' AS DateTime2), 10, N'En los servidores   dc2qvawsv1:5176, dc2qvawsv2:5176  realizar copia de seguridad de toda la aplicación "WcfTransverse" ubicada dentro del Sitio Web "WcfTransverse" (tomar ruta del IIS). Realizar copia en cada uno de los archivos y reemplazar en la carpeta bin que esta ubicado en la ruta \\dc2tvaweb5\Temp\RF-81142-1-42105\bin			', N'Admin. Servidores', N'Infraestructura', 15, NULL)
INSERT [dbo].[Plans] ([PlanID], [Sequence], [DataStartTime], [DataEndTime], [ExecutionTime], [Description], [Responsible], [supplierArea], [ChangeID], [ChangesChangeID]) VALUES (5, 4, CAST(N'2022-12-04T00:30:00.0000000' AS DateTime2), CAST(N'2022-12-04T00:50:00.0000000' AS DateTime2), 10, N'" En los servidores  dc2qvawsv1:5176, dc2qvawsv2:5176 entrar al webconfig del aplicativo y agregar lo siguiente en el webconfig  antes del cierre de la viñeta </log4net>
		<!--LOG SEGURIDAD POLIZAS AUTOS-->
    <logger name=""LogEmisionPolizas"">
      <level value=""ALL"" />
      <appender-ref ref=""LogPolizasEmision"" />
    </logger>
    <appender name=""LogPolizasEmision"" type=""log4net.Appender.RollingFileAppender"">
      <staticLogFileName value=""false"" />
      <rollingStyle value=""Date"" />
      <datePattern value=""_(dd-MM-yyyy)''.log''"" />
      <file value=""E:\\Log\\PolizasSolicitudesAutos\\seguridad-log-polizasSolicitudesAutos"" />
      <appendToFile value=""true"" />
      <lockingModel type=""log4net.Appender.FileAppender+MinimalLock"" />
      <layout type=""log4net.Layout.PatternLayout"">
        <conversionPattern value=""%newline %date [%thread] %-5level %logger - %message%newline"" />
      </layout>
    </appender>
"			', N'Admin. Servidores', N'Infraestructura', 15, NULL)
INSERT [dbo].[Plans] ([PlanID], [Sequence], [DataStartTime], [DataEndTime], [ExecutionTime], [Description], [Responsible], [supplierArea], [ChangeID], [ChangesChangeID]) VALUES (6, 5, CAST(N'2022-12-04T00:50:00.0000000' AS DateTime2), CAST(N'2022-12-04T02:50:00.0000000' AS DateTime2), 125, N'Solidificar (habilitar el solidcore) en los servidores  dc2qvawsv1, dc2qvawsv2			', N'Seguridad TI', N'Infraestructura', 15, NULL)
INSERT [dbo].[Plans] ([PlanID], [Sequence], [DataStartTime], [DataEndTime], [ExecutionTime], [Description], [Responsible], [supplierArea], [ChangeID], [ChangesChangeID]) VALUES (8, 6, CAST(N'2022-12-04T02:50:00.0000000' AS DateTime2), CAST(N'2022-12-04T03:15:00.0000000' AS DateTime2), 25, N'Tiempo reservado para rollback', N'Admin. Servidores', N'Infraestructura', 15, NULL)
SET IDENTITY_INSERT [dbo].[Plans] OFF
GO
SET IDENTITY_INSERT [dbo].[Postimplantacions] ON 

INSERT [dbo].[Postimplantacions] ([PostimplantacionID], [Sequence], [Description], [DataStartTime], [DataEndTime], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1, 1, N'Verificar funcionamiento tecnico del proceso			', CAST(N'2022-12-04T03:15:00.0000000' AS DateTime2), CAST(N'2022-12-04T21:35:00.0000000' AS DateTime2), 15, 1007, NULL)
INSERT [dbo].[Postimplantacions] ([PostimplantacionID], [Sequence], [Description], [DataStartTime], [DataEndTime], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1001, 2, N'Validacion', CAST(N'2023-12-12T00:00:00.0000000' AS DateTime2), CAST(N'2023-12-13T00:00:00.0000000' AS DateTime2), 19, 2, NULL)
SET IDENTITY_INSERT [dbo].[Postimplantacions] OFF
GO
SET IDENTITY_INSERT [dbo].[Prerrequisitos] ON 

INSERT [dbo].[Prerrequisitos] ([PrerrequisitoID], [Sequence], [Description], [DataStart], [DataEnd], [ExecutionTime], [SupplierArea], [Responsible], [ChangeID], [UserID], [ChangesChangeID]) VALUES (4, 1, N'Desolidificar los servidores', CAST(N'2023-07-07T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-08T00:00:00.0000000' AS DateTime2), 10, N'Seguridad TI', N'N/A', 19, 1, NULL)
INSERT [dbo].[Prerrequisitos] ([PrerrequisitoID], [Sequence], [Description], [DataStart], [DataEnd], [ExecutionTime], [SupplierArea], [Responsible], [ChangeID], [UserID], [ChangesChangeID]) VALUES (9, 2, N'Instalar los componentes de base de datos  ', CAST(N'2023-07-08T00:00:00.0000000' AS DateTime2), CAST(N'2023-07-09T00:00:00.0000000' AS DateTime2), 10, N'Infractructura', N'N/A', 19, 1002, NULL)
SET IDENTITY_INSERT [dbo].[Prerrequisitos] OFF
GO
SET IDENTITY_INSERT [dbo].[Rols] ON 

INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (1, N'Desarrollador', 1)
INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (2, N'Lider Web', 1)
INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (1003, N'Nivel 2', 1)
INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (1004, N'CAB', 1)
INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (1005, N'Lider Modelo de SAC', 1)
INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (1006, N'Release Manager', 1)
INSERT [dbo].[Rols] ([RolID], [TypeoRol], [State]) VALUES (1007, N'Ambientes QA', 1)
SET IDENTITY_INSERT [dbo].[Rols] OFF
GO
SET IDENTITY_INSERT [dbo].[Servers] ON 

INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (1, N'Servidor 1/1')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (2, N'Servidor 1/2')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (3, N'Servidor 2/1')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (4, N'Servidor 2/2')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (5, N'Servidor 3/1')
SET IDENTITY_INSERT [dbo].[Servers] OFF
GO
SET IDENTITY_INSERT [dbo].[Signatures] ON 

INSERT [dbo].[Signatures] ([SignatureID], [Observatins], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1, N'N/A', 15, 1002, NULL)
INSERT [dbo].[Signatures] ([SignatureID], [Observatins], [ChangeID], [UserID], [ChangesChangeID]) VALUES (2, N'N/A', 15, 1003, NULL)
INSERT [dbo].[Signatures] ([SignatureID], [Observatins], [ChangeID], [UserID], [ChangesChangeID]) VALUES (3, N'N/A', 15, 1004, NULL)
SET IDENTITY_INSERT [dbo].[Signatures] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainings] ON 

INSERT [dbo].[Trainings] ([TrainingID], [Comments], [DataTraining], [Type], [Objective], [Issues], [ChangeID], [UserID], [ChangesChangeID]) VALUES (21, N'aaaaaa', CAST(N'2022-07-20T00:00:00.0000000' AS DateTime2), N'Funcional', N'N/A', N'n/A', 19, 1002, NULL)
INSERT [dbo].[Trainings] ([TrainingID], [Comments], [DataTraining], [Type], [Objective], [Issues], [ChangeID], [UserID], [ChangesChangeID]) VALUES (29, N'vbbbb', CAST(N'2022-08-19T00:00:00.0000000' AS DateTime2), N'Funcional', N'N/A', N'N/A', 19, 2, NULL)
SET IDENTITY_INSERT [dbo].[Trainings] OFF
GO
SET IDENTITY_INSERT [dbo].[UserChanges] ON 

INSERT [dbo].[UserChanges] ([UserChangeID], [State], [ChangeID], [UserID]) VALUES (1, 1, 15, 1)
SET IDENTITY_INSERT [dbo].[UserChanges] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1, N'dvalenciac', N'Daniel Valencia', N'dfvalenciac@axacolpatria.co', 301416996, N'Digital', N'Desarrollador')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (2, N'jebellof', N'Juan Bello', N'jebellof@axacolpatria.co', 318531170, N'Digital', N'Desarrollador')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (3, N'jsuarez', N'Jairo Suarez', N'jairo.suarez@axacolpatria.co', 317425195, N'Digital', N'Lider Web')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (4, N'Nestor', N'Nestor Lasso', N'nestor.lasso@axacolpatria.co', 311573198, N'Digital', N'Lider Web')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (5, N'Fabio', N'Fabio Quinones', N'fabio.quinones@axacolpatria.co', 313287034, N'Digital', N'Desarrollador')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1002, N'Daniel', N'Daniel Herrera', N'daniel', 123, N'Tecnologia', N'Nivel 2')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1003, N'CAB', N'CAB', N'CAB', 123, N'CAB', N'CAB')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1004, N'Nancy', N'Nancy SEQUERA VERGARA', N'nancy@axacolpatria', 123, N'Digital', N'Lider Modelo de SAC')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1005, N'jaleman', N'Jhonatan Hernandez Aleman', N'jhonathan.hernandez@axacolpatria.co', 3364677, N'Digital', N'Release Manager')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1006, N'lguerrero', N'Luz Angela Guerrero', N'luz.guerrero@axacolpatria.co', 0, N'QA', N'Ambientes QA')
INSERT [dbo].[Users] ([UserID], [NetworkUser], [Name], [Email], [Phone], [Area], [Position]) VALUES (1007, N'Ecrojas', N'Edgar Camilo Rojas Peña', N'ecrojasp@axacolpatria.co', 305204717, N'Digital', N'Desarrollador_experto_.Net	')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Blueprints]  WITH CHECK ADD  CONSTRAINT [FK_Blueprints_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Blueprints] CHECK CONSTRAINT [FK_Blueprints_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Changes]  WITH CHECK ADD  CONSTRAINT [FK_Changes_Environments_EnvironmentID] FOREIGN KEY([EnvironmentID])
REFERENCES [dbo].[Environments] ([EnvironmentID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Changes] CHECK CONSTRAINT [FK_Changes_Environments_EnvironmentID]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Users_UserID]
GO
ALTER TABLE [dbo].[EnvironmentApplicatives]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentApplicatives_Applicatives_ApplicativeID] FOREIGN KEY([ApplicativeID])
REFERENCES [dbo].[Applicatives] ([ApplicativeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EnvironmentApplicatives] CHECK CONSTRAINT [FK_EnvironmentApplicatives_Applicatives_ApplicativeID]
GO
ALTER TABLE [dbo].[EnvironmentApplicatives]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentApplicatives_Environments_EnvironmentID] FOREIGN KEY([EnvironmentID])
REFERENCES [dbo].[Environments] ([EnvironmentID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EnvironmentApplicatives] CHECK CONSTRAINT [FK_EnvironmentApplicatives_Environments_EnvironmentID]
GO
ALTER TABLE [dbo].[EnvironmentApplicatives]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentApplicatives_Servers_ServerID] FOREIGN KEY([ServerID])
REFERENCES [dbo].[Servers] ([ServerID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EnvironmentApplicatives] CHECK CONSTRAINT [FK_EnvironmentApplicatives_Servers_ServerID]
GO
ALTER TABLE [dbo].[FunctionalUsers]  WITH CHECK ADD  CONSTRAINT [FK_FunctionalUsers_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FunctionalUsers] CHECK CONSTRAINT [FK_FunctionalUsers_Changes_ChangeID]
GO
ALTER TABLE [dbo].[FunctionalUsers]  WITH CHECK ADD  CONSTRAINT [FK_FunctionalUsers_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[FunctionalUsers] CHECK CONSTRAINT [FK_FunctionalUsers_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[FunctionalUsers]  WITH CHECK ADD  CONSTRAINT [FK_FunctionalUsers_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FunctionalUsers] CHECK CONSTRAINT [FK_FunctionalUsers_Users_UserID]
GO
ALTER TABLE [dbo].[Plans]  WITH CHECK ADD  CONSTRAINT [FK_Plans_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Plans] CHECK CONSTRAINT [FK_Plans_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Plans]  WITH CHECK ADD  CONSTRAINT [FK_Plans_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Plans] CHECK CONSTRAINT [FK_Plans_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[Postimplantacions]  WITH CHECK ADD  CONSTRAINT [FK_Postimplantacions_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Postimplantacions] CHECK CONSTRAINT [FK_Postimplantacions_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Postimplantacions]  WITH CHECK ADD  CONSTRAINT [FK_Postimplantacions_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Postimplantacions] CHECK CONSTRAINT [FK_Postimplantacions_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[Postimplantacions]  WITH CHECK ADD  CONSTRAINT [FK_Postimplantacions_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Postimplantacions] CHECK CONSTRAINT [FK_Postimplantacions_Users_UserID]
GO
ALTER TABLE [dbo].[Prerrequisitos]  WITH CHECK ADD  CONSTRAINT [FK_Prerrequisitos_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prerrequisitos] CHECK CONSTRAINT [FK_Prerrequisitos_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Prerrequisitos]  WITH CHECK ADD  CONSTRAINT [FK_Prerrequisitos_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Prerrequisitos] CHECK CONSTRAINT [FK_Prerrequisitos_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[Prerrequisitos]  WITH CHECK ADD  CONSTRAINT [FK_Prerrequisitos_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prerrequisitos] CHECK CONSTRAINT [FK_Prerrequisitos_Users_UserID]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[RolUsers]  WITH CHECK ADD  CONSTRAINT [FK_RolUsers_Rols_RolID] FOREIGN KEY([RolID])
REFERENCES [dbo].[Rols] ([RolID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolUsers] CHECK CONSTRAINT [FK_RolUsers_Rols_RolID]
GO
ALTER TABLE [dbo].[RolUsers]  WITH CHECK ADD  CONSTRAINT [FK_RolUsers_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolUsers] CHECK CONSTRAINT [FK_RolUsers_Users_UserID]
GO
ALTER TABLE [dbo].[Signatures]  WITH CHECK ADD  CONSTRAINT [FK_Signatures_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Signatures] CHECK CONSTRAINT [FK_Signatures_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Signatures]  WITH CHECK ADD  CONSTRAINT [FK_Signatures_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Signatures] CHECK CONSTRAINT [FK_Signatures_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[Signatures]  WITH CHECK ADD  CONSTRAINT [FK_Signatures_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Signatures] CHECK CONSTRAINT [FK_Signatures_Users_UserID]
GO
ALTER TABLE [dbo].[Trainings]  WITH CHECK ADD  CONSTRAINT [FK_Trainings_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trainings] CHECK CONSTRAINT [FK_Trainings_Changes_ChangeID]
GO
ALTER TABLE [dbo].[Trainings]  WITH CHECK ADD  CONSTRAINT [FK_Trainings_Changes_ChangesChangeID] FOREIGN KEY([ChangesChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
GO
ALTER TABLE [dbo].[Trainings] CHECK CONSTRAINT [FK_Trainings_Changes_ChangesChangeID]
GO
ALTER TABLE [dbo].[Trainings]  WITH CHECK ADD  CONSTRAINT [FK_Trainings_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trainings] CHECK CONSTRAINT [FK_Trainings_Users_UserID]
GO
ALTER TABLE [dbo].[UserChanges]  WITH CHECK ADD  CONSTRAINT [FK_UserChanges_Changes_ChangeID] FOREIGN KEY([ChangeID])
REFERENCES [dbo].[Changes] ([ChangeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserChanges] CHECK CONSTRAINT [FK_UserChanges_Changes_ChangeID]
GO
ALTER TABLE [dbo].[UserChanges]  WITH CHECK ADD  CONSTRAINT [FK_UserChanges_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserChanges] CHECK CONSTRAINT [FK_UserChanges_Users_UserID]
GO
