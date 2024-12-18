IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Tokens] (
        [Id] uniqueidentifier NOT NULL DEFAULT (newsequentialid()),
        [Action] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Tokens] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [AccountIndustries] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AccountIndustries] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Groups] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(150) NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Groups] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Stages] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Percentage] int NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Stages] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [UserRoles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Accounts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(200) NOT NULL,
        [Code] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [ContactDetails] nvarchar(max) NULL,
        [AccountIndustryId] int NULL,
        [TermsOfPayment] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Accounts_AccountIndustries_AccountIndustryId] FOREIGN KEY ([AccountIndustryId]) REFERENCES [AccountIndustries] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [ComponentTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_ComponentTypes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ComponentTypes_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Principals] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [GroupId] int NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Principals] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Principals_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [ObjectId] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(150) NOT NULL,
        [LastName] nvarchar(150) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [RoleId] int NULL,
        [SupervisorId] int NULL,
        [CreatedOn] datetime2 NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Users_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [UserRoles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Users_Users_SupervisorId] FOREIGN KEY ([SupervisorId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [AccountContacts] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [Designation] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [ContactDetails] nvarchar(100) NULL,
        [PrimaryContact] bit NOT NULL,
        [AccountId] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_AccountContacts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AccountContacts_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [PrincipalId] int NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Principals_PrincipalId] FOREIGN KEY ([PrincipalId]) REFERENCES [Principals] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Opportunities] (
        [Id] int NOT NULL IDENTITY,
        [AccountId] int NOT NULL,
        [BigDealCode] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        [CreatedById] int NOT NULL,
        [Modified] datetime2 NOT NULL,
        [ModifiedById] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_Opportunities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Opportunities_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Opportunities_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Opportunities_Users_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Tickets] (
        [Id] int NOT NULL IDENTITY,
        [Subject] nvarchar(250) NOT NULL,
        [Body] nvarchar(max) NOT NULL,
        [AccountId] int NOT NULL,
        [RequesterId] int NOT NULL,
        [AssigneeId] int NULL,
        [Status] int NOT NULL,
        [Type] int NOT NULL,
        [Priority] int NOT NULL,
        [Products] nvarchar(max) NULL,
        [RootCause] nvarchar(max) NULL,
        [Resolution] nvarchar(max) NULL,
        [ClosedOn] datetime2 NULL,
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tickets_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Tickets_Users_AssigneeId] FOREIGN KEY ([AssigneeId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Tickets_AccountContacts_RequesterId] FOREIGN KEY ([RequesterId]) REFERENCES [AccountContacts] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [ProductAssignments] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_ProductAssignments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductAssignments_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProductAssignments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [Components] (
        [Id] int NOT NULL IDENTITY,
        [OpportunityId] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [ComponentTypeId] int NULL,
        [AccountExecutiveId] int NOT NULL,
        [SolutionsArchitectId] int NOT NULL,
        [TargetCloseDate] datetime2 NULL,
        [StageId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Qty] int NOT NULL,
        [PricePerUnit] decimal(12, 4) NOT NULL,
        [CostPerUnit] decimal(12, 4) NOT NULL,
        [Poc] bit NOT NULL,
        [ValidityDate] datetime2 NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        [CreatedById] int NOT NULL,
        [Modified] datetime2 NOT NULL,
        [ModifiedById] int NOT NULL,
        [VersionNumber] int NOT NULL,
        CONSTRAINT [PK_Components] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Components_Users_AccountExecutiveId] FOREIGN KEY ([AccountExecutiveId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_ComponentTypes_ComponentTypeId] FOREIGN KEY ([ComponentTypeId]) REFERENCES [ComponentTypes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_Users_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_Opportunities_OpportunityId] FOREIGN KEY ([OpportunityId]) REFERENCES [Opportunities] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Components_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_Users_SolutionsArchitectId] FOREIGN KEY ([SolutionsArchitectId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Components_Stages_StageId] FOREIGN KEY ([StageId]) REFERENCES [Stages] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [TicketHistories] (
        [Id] int NOT NULL IDENTITY,
        [TicketId] int NOT NULL,
        [Body] nvarchar(max) NULL,
        [CreatedById] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_TicketHistories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TicketHistories_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TicketHistories_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [TicketProcedures] (
        [Id] int NOT NULL IDENTITY,
        [TicketId] int NOT NULL,
        [Body] nvarchar(max) NULL,
        [CreatedById] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_TicketProcedures] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TicketProcedures_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TicketProcedures_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE TABLE [ComponentVersions] (
        [Id] int NOT NULL IDENTITY,
        [ComponentId] int NOT NULL,
        [Added] datetime2 NOT NULL,
        [OpportunityId] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [ComponentTypeId] int NOT NULL,
        [AccountExecutiveId] int NOT NULL,
        [SolutionsArchitectId] int NOT NULL,
        [TargetCloseMonth] datetime2 NOT NULL,
        [StageId] int NOT NULL,
        [ProductId] int NOT NULL,
        [Qty] int NOT NULL,
        [PricePerUnit] decimal(12, 4) NOT NULL,
        [CostPerUnit] decimal(12, 4) NOT NULL,
        [POC] bit NOT NULL,
        [Validity] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Remarks] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        [CreatedById] int NOT NULL,
        [Modified] datetime2 NOT NULL,
        [ModifiedById] int NOT NULL,
        [VersionNumber] int NOT NULL,
        CONSTRAINT [PK_ComponentVersions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ComponentVersions_Components_ComponentId] FOREIGN KEY ([ComponentId]) REFERENCES [Components] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_AccountContacts_AccountId] ON [AccountContacts] ([AccountId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Accounts_AccountIndustryId] ON [Accounts] ([AccountIndustryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_AccountExecutiveId] ON [Components] ([AccountExecutiveId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_CategoryId] ON [Components] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_ComponentTypeId] ON [Components] ([ComponentTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_CreatedById] ON [Components] ([CreatedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_ModifiedById] ON [Components] ([ModifiedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_OpportunityId] ON [Components] ([OpportunityId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_ProductId] ON [Components] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_SolutionsArchitectId] ON [Components] ([SolutionsArchitectId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Components_StageId] ON [Components] ([StageId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_ComponentTypes_CategoryId] ON [ComponentTypes] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_ComponentVersions_ComponentId] ON [ComponentVersions] ([ComponentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Opportunities_AccountId] ON [Opportunities] ([AccountId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Opportunities_CreatedById] ON [Opportunities] ([CreatedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Opportunities_ModifiedById] ON [Opportunities] ([ModifiedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Principals_GroupId] ON [Principals] ([GroupId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Principals_Name] ON [Principals] ([Name]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_ProductAssignments_ProductId] ON [ProductAssignments] ([ProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_ProductAssignments_UserId] ON [ProductAssignments] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Products_Name] ON [Products] ([Name]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Products_PrincipalId] ON [Products] ([PrincipalId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_TicketHistories_CreatedById] ON [TicketHistories] ([CreatedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_TicketHistories_TicketId] ON [TicketHistories] ([TicketId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_TicketProcedures_CreatedById] ON [TicketProcedures] ([CreatedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_TicketProcedures_TicketId] ON [TicketProcedures] ([TicketId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Tickets_AccountId] ON [Tickets] ([AccountId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Tickets_AssigneeId] ON [Tickets] ([AssigneeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Tickets_RequesterId] ON [Tickets] ([RequesterId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Users_ObjectId] ON [Users] ([ObjectId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    CREATE INDEX [IX_Users_SupervisorId] ON [Users] ([SupervisorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190417062533_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190417062533_InitialCreate', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190426013153_AddTokenIdInOpportunityAndComponent')
BEGIN
    ALTER TABLE [Opportunities] ADD [TokenId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190426013153_AddTokenIdInOpportunityAndComponent')
BEGIN
    ALTER TABLE [Components] ADD [TokenId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190426013153_AddTokenIdInOpportunityAndComponent')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190426013153_AddTokenIdInOpportunityAndComponent', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190502024720_AddTokenIdInComponentVersion')
BEGIN
    ALTER TABLE [ComponentVersions] ADD [TokenId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190502024720_AddTokenIdInComponentVersion')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190502024720_AddTokenIdInComponentVersion', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190502052140_RemoveCreatedInComponentVersion')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ComponentVersions]') AND [c].[name] = N'Created');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ComponentVersions] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ComponentVersions] DROP COLUMN [Created];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190502052140_RemoveCreatedInComponentVersion')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ComponentVersions]') AND [c].[name] = N'CreatedById');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ComponentVersions] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [ComponentVersions] DROP COLUMN [CreatedById];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190502052140_RemoveCreatedInComponentVersion')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190502052140_RemoveCreatedInComponentVersion', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [ComponentVersions] DROP CONSTRAINT [FK_ComponentVersions_Components_ComponentId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    DROP INDEX [IX_ComponentVersions_ComponentId] ON [ComponentVersions];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tokens]') AND [c].[name] = N'Action');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Tokens] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Tokens] DROP COLUMN [Action];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [Tokens] ADD [CreatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [Tokens] ADD [TakeActionOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [Tokens] ADD [TokenAction] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [Tokens] ADD [TokenSubject] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [Tokens] ADD [TokenType] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [ComponentVersions] ADD [ComponentId1] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [Components] ADD [ComponentVersionId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    CREATE INDEX [IX_ComponentVersions_ComponentId1] ON [ComponentVersions] ([ComponentId1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    ALTER TABLE [ComponentVersions] ADD CONSTRAINT [FK_ComponentVersions_Components_ComponentId1] FOREIGN KEY ([ComponentId1]) REFERENCES [Components] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719050753_RecontructApprovalToken')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190719050753_RecontructApprovalToken', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719062847_UpdateToke')
BEGIN
    EXEC sp_rename N'[Tokens].[TakeActionOn]', N'ImplementedOn', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719062847_UpdateToke')
BEGIN
    ALTER TABLE [Tokens] ADD [ImplementedBy] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719062847_UpdateToke')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190719062847_UpdateToke', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719064635_AllowNullInComponentVersion')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Components]') AND [c].[name] = N'ComponentVersionId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Components] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Components] ALTER COLUMN [ComponentVersionId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190719064635_AllowNullInComponentVersion')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190719064635_AllowNullInComponentVersion', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023330_UpdateName')
BEGIN
    EXEC sp_rename N'[Opportunities].[TokenId]', N'RequestId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023330_UpdateName')
BEGIN
    EXEC sp_rename N'[Components].[TokenId]', N'RequestId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023330_UpdateName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190723023330_UpdateName', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023543_UpdateNameTokenToRequest')
BEGIN
    ALTER TABLE [Tokens] DROP CONSTRAINT [PK_Tokens];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023543_UpdateNameTokenToRequest')
BEGIN
    EXEC sp_rename N'[Tokens]', N'Request';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023543_UpdateNameTokenToRequest')
BEGIN
    ALTER TABLE [Request] ADD CONSTRAINT [PK_Request] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023543_UpdateNameTokenToRequest')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190723023543_UpdateNameTokenToRequest', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Request]') AND [c].[name] = N'TokenAction');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Request] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Request] DROP COLUMN [TokenAction];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Request]') AND [c].[name] = N'TokenSubject');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Request] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Request] DROP COLUMN [TokenSubject];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Request]') AND [c].[name] = N'TokenType');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Request] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Request] DROP COLUMN [TokenType];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    ALTER TABLE [Request] ADD [RequestAction] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    ALTER TABLE [Request] ADD [RequestSubject] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    ALTER TABLE [Request] ADD [RequestType] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190723023815_UpdateRequestName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190723023815_UpdateRequestName', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725025114_ChangeTokenIdToRequestIdInComponentVersion')
BEGIN
    EXEC sp_rename N'[ComponentVersions].[TokenId]', N'RequestId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725025114_ChangeTokenIdToRequestIdInComponentVersion')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190725025114_ChangeTokenIdToRequestIdInComponentVersion', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    ALTER TABLE [ComponentVersions] DROP CONSTRAINT [FK_ComponentVersions_Components_ComponentId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    DROP INDEX [IX_ComponentVersions_ComponentId1] ON [ComponentVersions];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ComponentVersions]') AND [c].[name] = N'ComponentId1');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ComponentVersions] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [ComponentVersions] DROP COLUMN [ComponentId1];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ComponentVersions]') AND [c].[name] = N'Validity');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ComponentVersions] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [ComponentVersions] ALTER COLUMN [Validity] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ComponentVersions]') AND [c].[name] = N'TargetCloseMonth');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [ComponentVersions] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [ComponentVersions] ALTER COLUMN [TargetCloseMonth] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ComponentVersions]') AND [c].[name] = N'ComponentTypeId');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [ComponentVersions] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [ComponentVersions] ALTER COLUMN [ComponentTypeId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190725073134_UpdatePropertyOfComponentVersion')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190725073134_UpdatePropertyOfComponentVersion', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731064657_AddBankAccount')
BEGIN
    ALTER TABLE [Users] ADD [BankAccountNo] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731064657_AddBankAccount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190731064657_AddBankAccount', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731093913_AddCashReimbursementEntities')
BEGIN
    CREATE TABLE [ExpenseCategory] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ExpenseCategory] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731093913_AddCashReimbursementEntities')
BEGIN
    CREATE TABLE [Expense] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ExpenseCategoryId] int NOT NULL,
        CONSTRAINT [PK_Expense] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Expense_ExpenseCategory_ExpenseCategoryId] FOREIGN KEY ([ExpenseCategoryId]) REFERENCES [ExpenseCategory] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731093913_AddCashReimbursementEntities')
BEGIN
    CREATE INDEX [IX_Expense_ExpenseCategoryId] ON [Expense] ([ExpenseCategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190731093913_AddCashReimbursementEntities')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190731093913_AddCashReimbursementEntities', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE TABLE [ReimbursementBatches] (
        [Id] int NOT NULL IDENTITY,
        [Status] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [RecordsCount] int NOT NULL,
        [TotalAmount] decimal(12, 4) NOT NULL,
        CONSTRAINT [PK_ReimbursementBatches] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE TABLE [UserReimbursements] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [ReimbursementBatchId] int NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        [ReimbursementPeriodStart] datetime2 NOT NULL,
        [ReimbursementPeriodEnd] datetime2 NOT NULL,
        [ReimbursementStatus] int NOT NULL,
        CONSTRAINT [PK_UserReimbursements] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserReimbursements_ReimbursementBatches_ReimbursementBatchId] FOREIGN KEY ([ReimbursementBatchId]) REFERENCES [ReimbursementBatches] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserReimbursements_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE TABLE [UserExpenses] (
        [Id] int NOT NULL IDENTITY,
        [UserReimbursementId] int NOT NULL,
        [ExepenseId] int NOT NULL,
        [Amount] decimal(12, 4) NOT NULL,
        [ExpenseDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserExpenses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserExpenses_Expense_ExepenseId] FOREIGN KEY ([ExepenseId]) REFERENCES [Expense] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserExpenses_UserReimbursements_UserReimbursementId] FOREIGN KEY ([UserReimbursementId]) REFERENCES [UserReimbursements] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE INDEX [IX_UserExpenses_ExepenseId] ON [UserExpenses] ([ExepenseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE INDEX [IX_UserExpenses_UserReimbursementId] ON [UserExpenses] ([UserReimbursementId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE INDEX [IX_UserReimbursements_ReimbursementBatchId] ON [UserReimbursements] ([ReimbursementBatchId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    CREATE INDEX [IX_UserReimbursements_UserId] ON [UserReimbursements] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801031443_CashReimbursement')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190801031443_CashReimbursement', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801032603_UserReimbursementNullReimbursementBatchId')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserReimbursements]') AND [c].[name] = N'ReimbursementBatchId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [UserReimbursements] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [UserReimbursements] ALTER COLUMN [ReimbursementBatchId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190801032603_UserReimbursementNullReimbursementBatchId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190801032603_UserReimbursementNullReimbursementBatchId', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190802063239_ChangeDecimalPlaces')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserExpenses]') AND [c].[name] = N'Amount');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [UserExpenses] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [UserExpenses] ALTER COLUMN [Amount] decimal(12, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190802063239_ChangeDecimalPlaces')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ReimbursementBatches]') AND [c].[name] = N'TotalAmount');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [ReimbursementBatches] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [ReimbursementBatches] ALTER COLUMN [TotalAmount] decimal(12, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190802063239_ChangeDecimalPlaces')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190802063239_ChangeDecimalPlaces', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190808033237_CashReimbursementChange')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserReimbursements]') AND [c].[name] = N'ReimbursementPeriodEnd');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [UserReimbursements] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [UserReimbursements] DROP COLUMN [ReimbursementPeriodEnd];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190808033237_CashReimbursementChange')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserExpenses]') AND [c].[name] = N'ExpenseDate');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [UserExpenses] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [UserExpenses] DROP COLUMN [ExpenseDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190808033237_CashReimbursementChange')
BEGIN
    EXEC sp_rename N'[UserReimbursements].[ReimbursementPeriodStart]', N'ReimbursementDate', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190808033237_CashReimbursementChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190808033237_CashReimbursementChange', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] DROP CONSTRAINT [FK_UserReimbursements_ReimbursementBatches_ReimbursementBatchId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] DROP CONSTRAINT [FK_UserReimbursements_Users_UserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    DROP TABLE [ReimbursementBatches];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    DROP INDEX [IX_UserReimbursements_ReimbursementBatchId] ON [UserReimbursements];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    DROP INDEX [IX_UserReimbursements_UserId] ON [UserReimbursements];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserReimbursements]') AND [c].[name] = N'ReimbursementBatchId');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [UserReimbursements] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [UserReimbursements] DROP COLUMN [ReimbursementBatchId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] ADD [CreatedById] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] ADD [ProcessById] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] ADD [Remarks] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    CREATE UNIQUE INDEX [IX_UserReimbursements_CreatedById] ON [UserReimbursements] ([CreatedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    CREATE UNIQUE INDEX [IX_UserReimbursements_ProcessById] ON [UserReimbursements] ([ProcessById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    CREATE UNIQUE INDEX [IX_UserReimbursements_UserId] ON [UserReimbursements] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] ADD CONSTRAINT [FK_UserReimbursements_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] ADD CONSTRAINT [FK_UserReimbursements_Users_ProcessById] FOREIGN KEY ([ProcessById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    ALTER TABLE [UserReimbursements] ADD CONSTRAINT [FK_UserReimbursements_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190813061201_RemoveReimbursementBatch')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190813061201_RemoveReimbursementBatch', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814031139_UserReimbursementSetSomePropertyNull')
BEGIN
    DROP INDEX [IX_UserReimbursements_ProcessById] ON [UserReimbursements];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814031139_UserReimbursementSetSomePropertyNull')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserReimbursements]') AND [c].[name] = N'ProcessById');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [UserReimbursements] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [UserReimbursements] ALTER COLUMN [ProcessById] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814031139_UserReimbursementSetSomePropertyNull')
BEGIN
    ALTER TABLE [UserReimbursements] ADD [ProcessOn] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814031139_UserReimbursementSetSomePropertyNull')
BEGIN
    ALTER TABLE [UserReimbursements] ADD [ReferenceNumber] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814031139_UserReimbursementSetSomePropertyNull')
BEGIN
    CREATE UNIQUE INDEX [IX_UserReimbursements_ProcessById] ON [UserReimbursements] ([ProcessById]) WHERE [ProcessById] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190814031139_UserReimbursementSetSomePropertyNull')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190814031139_UserReimbursementSetSomePropertyNull', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    DROP INDEX [IX_UserReimbursements_CreatedById] ON [UserReimbursements];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    DROP INDEX [IX_UserReimbursements_ProcessById] ON [UserReimbursements];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    DROP INDEX [IX_UserReimbursements_UserId] ON [UserReimbursements];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    CREATE INDEX [IX_UserReimbursements_CreatedById] ON [UserReimbursements] ([CreatedById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    CREATE INDEX [IX_UserReimbursements_ProcessById] ON [UserReimbursements] ([ProcessById]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    CREATE INDEX [IX_UserReimbursements_UserId] ON [UserReimbursements] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190816015733_UpdateUserReimbursementRelationship')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190816015733_UpdateUserReimbursementRelationship', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819015016_AddReimbursee')
BEGIN
    ALTER TABLE [UserReimbursements] DROP CONSTRAINT [FK_UserReimbursements_Users_UserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819015016_AddReimbursee')
BEGIN
    EXEC sp_rename N'[UserReimbursements].[UserId]', N'ReimburseeId', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819015016_AddReimbursee')
BEGIN
    EXEC sp_rename N'[UserReimbursements].[IX_UserReimbursements_UserId]', N'IX_UserReimbursements_ReimburseeId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819015016_AddReimbursee')
BEGIN
    CREATE TABLE [Reimbursees] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [BankAccountNumber] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_Reimbursees] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819015016_AddReimbursee')
BEGIN
    ALTER TABLE [UserReimbursements] ADD CONSTRAINT [FK_UserReimbursements_Reimbursees_ReimburseeId] FOREIGN KEY ([ReimburseeId]) REFERENCES [Reimbursees] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819015016_AddReimbursee')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190819015016_AddReimbursee', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819030617_RemoveEmailInReimbursee')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reimbursees]') AND [c].[name] = N'Email');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Reimbursees] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Reimbursees] DROP COLUMN [Email];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190819030617_RemoveEmailInReimbursee')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190819030617_RemoveEmailInReimbursee', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190822014456_AddUsePermissions')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'BankAccountNo');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Users] DROP COLUMN [BankAccountNo];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190822014456_AddUsePermissions')
BEGIN
    CREATE TABLE [UserPermissions] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Permission] int NOT NULL,
        CONSTRAINT [PK_UserPermissions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserPermissions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190822014456_AddUsePermissions')
BEGIN
    CREATE INDEX [IX_UserPermissions_UserId] ON [UserPermissions] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190822014456_AddUsePermissions')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190822014456_AddUsePermissions', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190829074126_UpdateUserExpense')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserReimbursements]') AND [c].[name] = N'Remarks');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [UserReimbursements] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [UserReimbursements] DROP COLUMN [Remarks];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190829074126_UpdateUserExpense')
BEGIN
    ALTER TABLE [UserExpenses] ADD [Date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190829074126_UpdateUserExpense')
BEGIN
    ALTER TABLE [UserExpenses] ADD [Remarks] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190829074126_UpdateUserExpense')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190829074126_UpdateUserExpense', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190913032207_AddPOPendingItem')
BEGIN
    CREATE TABLE [POPendingItems] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Price] float NOT NULL,
        [Quantity] int NOT NULL,
        [Total] float NOT NULL,
        CONSTRAINT [PK_POPendingItems] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190913032207_AddPOPendingItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190913032207_AddPOPendingItem', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190920082247_AddPOPending')
BEGIN
    ALTER TABLE [POPendingItems] ADD [POPendingId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190920082247_AddPOPending')
BEGIN
    CREATE TABLE [POPending] (
        [Id] int NOT NULL IDENTITY,
        [ReferenceNumber] nvarchar(max) NOT NULL,
        [Supplier] nvarchar(max) NOT NULL,
        [ContactPerson] nvarchar(max) NOT NULL,
        [Customer] nvarchar(max) NOT NULL,
        [EstimatedArrival] datetime2 NOT NULL,
        [AccountExecutive] nvarchar(max) NOT NULL,
        [Currency] nvarchar(max) NOT NULL,
        [Approver] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_POPending] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190920082247_AddPOPending')
BEGIN
    CREATE INDEX [IX_POPendingItems_POPendingId] ON [POPendingItems] ([POPendingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190920082247_AddPOPending')
BEGIN
    ALTER TABLE [POPendingItems] ADD CONSTRAINT [FK_POPendingItems_POPending_POPendingId] FOREIGN KEY ([POPendingId]) REFERENCES [POPending] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190920082247_AddPOPending')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190920082247_AddPOPending', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    ALTER TABLE [POPendingItems] DROP CONSTRAINT [FK_POPendingItems_POPending_POPendingId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    ALTER TABLE [POPending] DROP CONSTRAINT [PK_POPending];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    EXEC sp_rename N'[POPending]', N'POPendings';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    EXEC sp_rename N'[POPendings].[Approver]', N'ApproverName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'EstimatedArrival');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [EstimatedArrival] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    ALTER TABLE [POPendings] ADD [ApproverEmail] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    ALTER TABLE [POPendings] ADD CONSTRAINT [PK_POPendings] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    ALTER TABLE [POPendingItems] ADD CONSTRAINT [FK_POPendingItems_POPendings_POPendingId] FOREIGN KEY ([POPendingId]) REFERENCES [POPendings] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924072206_POPendingAddApproverEmail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190924072206_POPendingAddApproverEmail', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    EXEC sp_rename N'[POPendings].[Supplier]', N'SupplierName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    EXEC sp_rename N'[POPendings].[Customer]', N'CustomerName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    EXEC sp_rename N'[POPendings].[ContactPerson]', N'ContactPersonName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    EXEC sp_rename N'[POPendings].[AccountExecutive]', N'AccountExecutiveName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    ALTER TABLE [POPendings] ADD [AccountExecutiveId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    ALTER TABLE [POPendings] ADD [ApproverId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    ALTER TABLE [POPendings] ADD [ContactPersonId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    ALTER TABLE [POPendings] ADD [CustomerId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    ALTER TABLE [POPendings] ADD [SupplierId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190924081812_POPendingColumnsId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190924081812_POPendingColumnsId', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [CreatedById] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [CreatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [EstimatedArrivalString] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [InternalNote] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [Remarks] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [RequestorEmail] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [Status] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    ALTER TABLE [POPendings] ADD [Total] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190925011826_POPendingUpdates1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190925011826_POPendingUpdates1', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'AccountExecutiveId');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [POPendings] DROP COLUMN [AccountExecutiveId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'AccountExecutiveName');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [POPendings] DROP COLUMN [AccountExecutiveName];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'CustomerId');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [POPendings] DROP COLUMN [CustomerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'SupplierName');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [SupplierName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'ReferenceNumber');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [ReferenceNumber] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'CustomerName');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [CustomerName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'Currency');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [Currency] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'ContactPersonName');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [ContactPersonName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'ApproverName');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [ApproverName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'ApproverEmail');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [ApproverEmail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    ALTER TABLE [POPendings] ADD [POPendingItemsJsonString] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    ALTER TABLE [POPendings] ADD [SupplierAddress] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927091037_PONullableFieldsForDraft')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190927091037_PONullableFieldsForDraft', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927093529_PONullableInt')
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'SupplierId');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [SupplierId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927093529_PONullableInt')
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'ContactPersonId');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [ContactPersonId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927093529_PONullableInt')
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'ApproverId');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [ApproverId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190927093529_PONullableInt')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190927093529_PONullableInt', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930064340_PoPendingAddDicount')
BEGIN
    ALTER TABLE [POPendings] ADD [CreatedByName] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930064340_PoPendingAddDicount')
BEGIN
    ALTER TABLE [POPendings] ADD [Discount] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930064340_PoPendingAddDicount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190930064340_PoPendingAddDicount', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930064833_PoPendingDicountChange')
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'Discount');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [Discount] float NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930064833_PoPendingDicountChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190930064833_PoPendingDicountChange', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930073332_PoPendingCreatedByNameChange')
BEGIN
    DECLARE @var36 sysname;
    SELECT @var36 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'CreatedByName');
    IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var36 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [CreatedByName] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190930073332_PoPendingCreatedByNameChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190930073332_PoPendingCreatedByNameChange', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191010054606_AddGuidInPOPending')
BEGIN
    ALTER TABLE [POPendings] ADD [Guid] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191010054606_AddGuidInPOPending')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191010054606_AddGuidInPOPending', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191125085358_AddPOGuidStatus')
BEGIN
    CREATE TABLE [POAuditTrails] (
        [Id] int NOT NULL IDENTITY,
        [POPendingId] int NOT NULL,
        [UserId] int NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [DateAdded] datetime2 NOT NULL,
        CONSTRAINT [PK_POAuditTrails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_POAuditTrails_POPendings_POPendingId] FOREIGN KEY ([POPendingId]) REFERENCES [POPendings] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_POAuditTrails_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191125085358_AddPOGuidStatus')
BEGIN
    CREATE TABLE [POGuidStatus] (
        [Id] int NOT NULL IDENTITY,
        [POGuid] uniqueidentifier NOT NULL,
        [POStatus] int NOT NULL,
        [AddedOn] datetime2 NOT NULL,
        [ModifiedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_POGuidStatus] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191125085358_AddPOGuidStatus')
BEGIN
    CREATE INDEX [IX_POAuditTrails_POPendingId] ON [POAuditTrails] ([POPendingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191125085358_AddPOGuidStatus')
BEGIN
    CREATE INDEX [IX_POAuditTrails_UserId] ON [POAuditTrails] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191125085358_AddPOGuidStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191125085358_AddPOGuidStatus', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191226030928_AddRequestorNamePOData')
BEGIN
    ALTER TABLE [POGuidStatus] ADD [POData] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191226030928_AddRequestorNamePOData')
BEGIN
    ALTER TABLE [POGuidStatus] ADD [RequestorEmail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191226030928_AddRequestorNamePOData')
BEGIN
    ALTER TABLE [POGuidStatus] ADD [RequestorName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191226030928_AddRequestorNamePOData')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191226030928_AddRequestorNamePOData', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    ALTER TABLE [POPendings] ADD [ApprovedOn] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    ALTER TABLE [POPendings] ADD [CancelledOn] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    ALTER TABLE [POPendings] ADD [HasBeenApproved] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    ALTER TABLE [POPendings] ADD [ReceivedOn] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    ALTER TABLE [POPendings] ADD [TextLineBreakCount] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    CREATE TABLE [POAttachment] (
        [Id] int NOT NULL IDENTITY,
        [POGuidStatusId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [File] varbinary(max) NOT NULL,
        [Size] bigint NOT NULL,
        [ContentType] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_POAttachment] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_POAttachment_POGuidStatus_POGuidStatusId] FOREIGN KEY ([POGuidStatusId]) REFERENCES [POGuidStatus] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    CREATE INDEX [IX_POAttachment_POGuidStatusId] ON [POAttachment] ([POGuidStatusId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106054548_AddPOAttachment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200106054548_AddPOAttachment', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106062123_UpdatePOAttachmentName')
BEGIN
    DROP TABLE [POAttachment];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106062123_UpdatePOAttachmentName')
BEGIN
    CREATE TABLE [POGuidStatusAttachments] (
        [Id] int NOT NULL IDENTITY,
        [POGuidStatusId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [File] varbinary(max) NOT NULL,
        [Size] bigint NOT NULL,
        [ContentType] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        CONSTRAINT [PK_POGuidStatusAttachments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_POGuidStatusAttachments_POGuidStatus_POGuidStatusId] FOREIGN KEY ([POGuidStatusId]) REFERENCES [POGuidStatus] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106062123_UpdatePOAttachmentName')
BEGIN
    CREATE INDEX [IX_POGuidStatusAttachments_POGuidStatusId] ON [POGuidStatusAttachments] ([POGuidStatusId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200106062123_UpdatePOAttachmentName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200106062123_UpdatePOAttachmentName', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    DECLARE @var37 sysname;
    SELECT @var37 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'Total');
    IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var37 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [Total] decimal(18, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    DECLARE @var38 sysname;
    SELECT @var38 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendings]') AND [c].[name] = N'Discount');
    IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [POPendings] DROP CONSTRAINT [' + @var38 + '];');
    ALTER TABLE [POPendings] ALTER COLUMN [Discount] decimal(18, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    ALTER TABLE [POPendings] ADD [ApproverJobTitle] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    ALTER TABLE [POPendings] ADD [RequestorName] nvarchar(max) NOT NULL DEFAULT N'';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    DECLARE @var39 sysname;
    SELECT @var39 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendingItems]') AND [c].[name] = N'Total');
    IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [POPendingItems] DROP CONSTRAINT [' + @var39 + '];');
    ALTER TABLE [POPendingItems] ALTER COLUMN [Total] decimal(18, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    DECLARE @var40 sysname;
    SELECT @var40 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[POPendingItems]') AND [c].[name] = N'Price');
    IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [POPendingItems] DROP CONSTRAINT [' + @var40 + '];');
    ALTER TABLE [POPendingItems] ALTER COLUMN [Price] decimal(18, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    ALTER TABLE [POGuidStatus] ADD [SendTOs] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200129033220_UpdatePOGuidStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200129033220_UpdatePOGuidStatus', N'2.1.11-servicing-32099');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200218094202_AddTrackerId')
BEGIN
    ALTER TABLE [POGuidStatus] ADD [TrackerId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200218094202_AddTrackerId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200218094202_AddTrackerId', N'2.1.11-servicing-32099');
END;

GO

