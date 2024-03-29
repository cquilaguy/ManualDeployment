USE [ManualDeploymentRazor]
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
SET IDENTITY_INSERT [dbo].[Environments] ON 

INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (1, N'PDC')
INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (2, N'TST')
INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (3, N'UAT')
INSERT [dbo].[Environments] ([EnvironmentID], [NameEnvironment]) VALUES (1002, N'')
SET IDENTITY_INSERT [dbo].[Environments] OFF
GO
SET IDENTITY_INSERT [dbo].[Servers] ON 

INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (1, N'Servidor 1/1')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (2, N'Servidor 1/2')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (3, N'Servidor 2/1')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (4, N'Servidor 2/2')
INSERT [dbo].[Servers] ([ServerID], [NameServer]) VALUES (5, N'Servidor 3/1')
SET IDENTITY_INSERT [dbo].[Servers] OFF
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
SET IDENTITY_INSERT [dbo].[Changes] ON 

INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (15, 1, N'RF-01', N'Requerimiento', N'CHG-01', 1, CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-03-17T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-10T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), N'Normal', N'En caso de falla en el punto 2 devolver al valor ', 1, 1, 1)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (19, 1, N'RF-02', N'Mantenimiento', N'CHG-02', 1, CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T00:00:00.0000000' AS DateTime2), N'Estandar', N'En caso de falla en el punto 2 devolver al valor ', 2, 0, 2)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (1002, 1, N'RF-81142-1-42105- LOG PROCESO DE ENVÍO DE CORREOS DESDE EL GESTOR DE VENTAS DE AUTOS', N'Requerimiento', N'CHG-89402-1-2420', 0, CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2023-03-17T00:00:00.0000000' AS DateTime2), CAST(N'2019-03-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-03-13T00:00:00.0000000' AS DateTime2), N'Estandar', N'En caso de falla en el punto 2 devolver al valor ', 0, 1, 1)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (1003, 0, N'RF-81142-1-42161-', N'Funcional', N'CHG-89402-1-42161', 1, CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), N'2', N'2', 2, 1, 2)
INSERT [dbo].[Changes] ([ChangeID], [State], [changeDescription], [requestType], [changerNumber], [checkList], [StartDate], [EndDate], [creationDate], [modificationDate], [applicationDate], [deploymentDate], [changeType], [Rollback], [Version], [IsTemplate], [EnvironmentID]) VALUES (2004, 1, N'RF-05', N'Funcional', N'CHG-05', 1, CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2022-04-13T00:00:00.0000000' AS DateTime2), N'Estandar', N'En caso de fallo', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Changes] OFF
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
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([ContactID], [Observations], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1, N'N/A', 15, 3, NULL)
INSERT [dbo].[Contacts] ([ContactID], [Observations], [ChangeID], [UserID], [ChangesChangeID]) VALUES (2, N'N/A', 15, 1007, NULL)
INSERT [dbo].[Contacts] ([ContactID], [Observations], [ChangeID], [UserID], [ChangesChangeID]) VALUES (1001, N'N/A', 1002, 3, NULL)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[FunctionalUsers] ON 

INSERT [dbo].[FunctionalUsers] ([FunctionalUserID], [Sequence], [DataStartTime], [DataEndTime], [ChangeID], [UserID], [ChangesChangeID]) VALUES (2, 1, CAST(N'2023-11-20T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-21T00:00:00.0000000' AS DateTime2), 19, 1002, NULL)
INSERT [dbo].[FunctionalUsers] ([FunctionalUserID], [Sequence], [DataStartTime], [DataEndTime], [ChangeID], [UserID], [ChangesChangeID]) VALUES (3, 2, CAST(N'2023-11-22T00:00:00.0000000' AS DateTime2), CAST(N'2023-11-23T00:00:00.0000000' AS DateTime2), 19, 1004, NULL)
SET IDENTITY_INSERT [dbo].[FunctionalUsers] OFF
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
SET IDENTITY_INSERT [dbo].[Blueprints] ON 

INSERT [dbo].[Blueprints] ([BlueprintID], [Version], [Date], [Route], [ChangeID]) VALUES (2, 11, CAST(N'2023-07-20T00:00:00.0000000' AS DateTime2), N'https://asesorescolpatria.sharepoint.com/:u:/s/Tecnologia/EVvFp-tVp5FJngwpkH1Ah04BK9rxR-mhQ-EuGheRM9j6rg?e=YeOAzn', 15)
SET IDENTITY_INSERT [dbo].[Blueprints] OFF
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230526150326_DBV1', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230705131146_V2', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230718163956__V3', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230726203901__V5', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230726205315__V5', N'5.0.17')
GO
