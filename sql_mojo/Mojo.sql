-- =============================================================
-- PROJET : Mojo Vélo - Base de Données Unifiée (V8.0)
-- ARCHITECTURE : Table Users (Pluriel), Stock Mojo, Amortissement
-- =============================================================

-- 1. Organisation
CREATE TABLE Organisation (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nom NVARCHAR(200) NOT NULL,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Adresse NVARCHAR(500),
    ContactEmail NVARCHAR(255),
    Actif BIT DEFAULT 1
);

-- 2. Users (Mojo Staff, HR Admin, Employés)
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrganisationId INT NOT NULL,
    Nom NVARCHAR(100) NOT NULL,
    Prenom NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    MotDePasseHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- 'MOJO_ADMIN', 'HR_ADMIN', 'EMPLOYEE'
    TailleCm INT NULL,
    Actif BIT DEFAULT 1,
    CONSTRAINT FK_Users_Org FOREIGN KEY (OrganisationId) REFERENCES Organisation(Id)
);

-- 3. Velo
CREATE TABLE Velo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroSerie NVARCHAR(100) UNIQUE NOT NULL,
    Marque NVARCHAR(100) NOT NULL,
    Modele NVARCHAR(200) NOT NULL,
    PrixAchatMojo DECIMAL(10,2) NOT NULL,
    Statut NVARCHAR(50) DEFAULT 'disponible'
);

-- 4. Amortissement
CREATE TABLE Amortissement (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VeloId INT NOT NULL,
    DateDebut DATE NOT NULL,
    ValeurInitiale DECIMAL(10,2) NOT NULL,
    DureeMois INT DEFAULT 48,
    ValeurResiduelleFinale DECIMAL(10,2) DEFAULT 0.00,
    CONSTRAINT FK_Amort_Velo FOREIGN KEY (VeloId) REFERENCES Velo(Id)
);

-- 5. Contrat
CREATE TABLE Contrat (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CreatedById INT NOT NULL,    -- Le HR_ADMIN
    BeneficiaireId INT NOT NULL, -- L'EMPLOYEE
    VeloId INT NOT NULL,
    DateDebut DATE NOT NULL,
    DateFin DATE NOT NULL,
    LoyerMensuelHT DECIMAL(10,2) NOT NULL,
    StatutContrat NVARCHAR(50) DEFAULT 'actif',
    CONSTRAINT FK_Contrat_Creator FOREIGN KEY (CreatedById) REFERENCES Users(Id),
    CONSTRAINT FK_Contrat_User FOREIGN KEY (BeneficiaireId) REFERENCES Users(Id),
    CONSTRAINT FK_Contrat_Velo FOREIGN KEY (VeloId) REFERENCES Velo(Id)
);

-- 6. Accessoire et liaison
CREATE TABLE Accessoire (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nom NVARCHAR(100) NOT NULL,
    PrixAchat DECIMAL(10,2),
    StockTotal INT DEFAULT 0
);

CREATE TABLE Contrat_accessoire (
    ContratId INT NOT NULL,
    AccessoireId INT NOT NULL,
    Quantite INT DEFAULT 1,
    PRIMARY KEY (ContratId, AccessoireId),
    FOREIGN KEY (ContratId) REFERENCES Contrat(Id),
    FOREIGN KEY (AccessoireId) REFERENCES Accessoire(Id)
);

-- 7. Intervention
CREATE TABLE Intervention (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VeloId INT NOT NULL,
    DateIntervention DATETIME2 NOT NULL,
    TypeIntervention NVARCHAR(50), 
    Description NVARCHAR(MAX),
    CoutPourMojo DECIMAL(10,2) DEFAULT 0.00,
    CONSTRAINT FK_Interv_Velo FOREIGN KEY (VeloId) REFERENCES Velo(Id)
);

-- 8. Discussion et Message
CREATE TABLE Discussion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    InitiateurId INT NOT NULL,
    Objet NVARCHAR(200),
    Statut NVARCHAR(20) DEFAULT 'ouvert',
    DateCreation DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_Disc_Users FOREIGN KEY (InitiateurId) REFERENCES Users(Id)
);

CREATE TABLE Message (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DiscussionId INT NOT NULL,
    ExpediteurId INT NOT NULL,
    Contenu NVARCHAR(MAX) NOT NULL,
    DateEnvoi DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_Msg_Disc FOREIGN KEY (DiscussionId) REFERENCES Discussion(Id),
    CONSTRAINT FK_Msg_Sender FOREIGN KEY (ExpediteurId) REFERENCES Users(Id)
);