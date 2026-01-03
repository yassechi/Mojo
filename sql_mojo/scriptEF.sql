IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Accessoires] (
    [Id] int NOT NULL IDENTITY,
    [Nom] nvarchar(max) NOT NULL,
    [PrixAchat] decimal(18,2) NULL,
    [StockTotal] int NOT NULL,
    CONSTRAINT [PK_Accessoires] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Organisations] (
    [Id] int NOT NULL IDENTITY,
    [Nom] nvarchar(200) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [Adresse] nvarchar(max) NULL,
    [ContactEmail] nvarchar(max) NULL,
    [Actif] bit NOT NULL,
    CONSTRAINT [PK_Organisations] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Velos] (
    [Id] int NOT NULL IDENTITY,
    [NumeroSerie] nvarchar(100) NOT NULL,
    [Marque] nvarchar(100) NOT NULL,
    [Modele] nvarchar(200) NOT NULL,
    [PrixAchatMojo] decimal(10,2) NOT NULL,
    [Statut] nvarchar(50) NOT NULL DEFAULT N'Disponible',
    CONSTRAINT [PK_Velos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [OrganisationId] int NOT NULL,
    [Nom] nvarchar(100) NOT NULL,
    [Prenom] nvarchar(100) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [MotDePasseHash] nvarchar(255) NOT NULL,
    [Role] nvarchar(20) NOT NULL,
    [TailleCm] int NULL,
    [Actif] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Organisations_OrganisationId] FOREIGN KEY ([OrganisationId]) REFERENCES [Organisations] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Amortissements] (
    [Id] int NOT NULL IDENTITY,
    [VeloId] int NOT NULL,
    [DateDebut] datetime2 NOT NULL,
    [ValeurInitiale] decimal(18,2) NOT NULL,
    [DureeMois] int NOT NULL,
    [ValeurResiduelleFinal] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Amortissements] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Amortissements_Velos_VeloId] FOREIGN KEY ([VeloId]) REFERENCES [Velos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Contrats] (
    [Id] int NOT NULL IDENTITY,
    [CreatedById] int NOT NULL,
    [BeneficiaireId] int NOT NULL,
    [VeloId] int NOT NULL,
    [DateDebut] datetime2 NOT NULL,
    [DateFin] datetime2 NOT NULL,
    [LoyerMensuelHT] decimal(10,2) NOT NULL,
    [StatutContrat] int NOT NULL,
    CONSTRAINT [PK_Contrats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contrats_Users_BeneficiaireId] FOREIGN KEY ([BeneficiaireId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Contrats_Users_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Contrats_Velos_VeloId] FOREIGN KEY ([VeloId]) REFERENCES [Velos] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Contrat_Accessoires] (
    [ContratId] int NOT NULL,
    [AccessoireId] int NOT NULL,
    [Quantite] int NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Contrat_Accessoires] PRIMARY KEY ([ContratId], [AccessoireId]),
    CONSTRAINT [FK_Contrat_Accessoires_Accessoires_AccessoireId] FOREIGN KEY ([AccessoireId]) REFERENCES [Accessoires] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Contrat_Accessoires_Contrats_ContratId] FOREIGN KEY ([ContratId]) REFERENCES [Contrats] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Discussions] (
    [Id] int NOT NULL IDENTITY,
    [ContratId] int NULL,
    [Sujet] nvarchar(max) NOT NULL,
    [DateCreation] datetime2 NOT NULL,
    [Statut] int NOT NULL,
    CONSTRAINT [PK_Discussions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Discussions_Contrats_ContratId] FOREIGN KEY ([ContratId]) REFERENCES [Contrats] ([Id])
);
GO

CREATE TABLE [Interventions] (
    [Id] int NOT NULL IDENTITY,
    [VeloId] int NOT NULL,
    [ContratId] int NULL,
    [DateIntervention] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    [Cout] decimal(18,2) NOT NULL,
    [TypeIntervention] int NOT NULL,
    CONSTRAINT [PK_Interventions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Interventions_Contrats_ContratId] FOREIGN KEY ([ContratId]) REFERENCES [Contrats] ([Id]),
    CONSTRAINT [FK_Interventions_Velos_VeloId] FOREIGN KEY ([VeloId]) REFERENCES [Velos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [DiscussionId] int NOT NULL,
    [AuteurId] int NOT NULL,
    [Contenu] nvarchar(max) NOT NULL,
    [DateEnvoi] datetime2 NOT NULL,
    [UserId] int NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_Discussions_DiscussionId] FOREIGN KEY ([DiscussionId]) REFERENCES [Discussions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Messages_Users_AuteurId] FOREIGN KEY ([AuteurId]) REFERENCES [Users] ([Id]),
    CONSTRAINT [FK_Messages_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE INDEX [IX_Amortissements_VeloId] ON [Amortissements] ([VeloId]);
GO

CREATE INDEX [IX_Contrat_Accessoires_AccessoireId] ON [Contrat_Accessoires] ([AccessoireId]);
GO

CREATE INDEX [IX_Contrats_BeneficiaireId] ON [Contrats] ([BeneficiaireId]);
GO

CREATE INDEX [IX_Contrats_CreatedById] ON [Contrats] ([CreatedById]);
GO

CREATE INDEX [IX_Contrats_VeloId] ON [Contrats] ([VeloId]);
GO

CREATE INDEX [IX_Discussions_ContratId] ON [Discussions] ([ContratId]);
GO

CREATE INDEX [IX_Interventions_ContratId] ON [Interventions] ([ContratId]);
GO

CREATE INDEX [IX_Interventions_VeloId] ON [Interventions] ([VeloId]);
GO

CREATE INDEX [IX_Messages_AuteurId] ON [Messages] ([AuteurId]);
GO

CREATE INDEX [IX_Messages_DiscussionId] ON [Messages] ([DiscussionId]);
GO

CREATE INDEX [IX_Messages_UserId] ON [Messages] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Organisations_Code] ON [Organisations] ([Code]);
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

CREATE INDEX [IX_Users_OrganisationId] ON [Users] ([OrganisationId]);
GO

CREATE UNIQUE INDEX [IX_Velos_NumeroSerie] ON [Velos] ([NumeroSerie]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251224185219_init', N'8.0.0');
GO

COMMIT;
GO

