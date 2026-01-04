-- NETTOYAGE COMPLET (Ordre respectant les dépendances)
DELETE FROM AccessoireContrat; 
DELETE FROM Messages; 
DELETE FROM Discussions; 
DELETE FROM Interventions; 
DELETE FROM Contrats; 
DELETE FROM Amortissements; 
DELETE FROM Users; 
DELETE FROM Velos; 
DELETE FROM Organisations; 
DELETE FROM Accessoires;

BEGIN TRANSACTION;

-- 1. ORGANISATIONS
SET IDENTITY_INSERT [Organisations] ON;
INSERT INTO [Organisations] ([Id], [Name], [Code], [Address], [ContactEmail], [IsActif]) VALUES 
(1, N'Mojo Corporate', N'MOJO_HQ', N'1 Rue de la Pompe, Paris', N'hq@mojo.com', 1),
(2, N'Vélo Libre Services', N'VLS_01', N'45 Avenue des Champs, Lyon', N'contact@vls.fr', 1),
(3, N'Eco-Rider Nantes', N'ECO_RS', N'10 Rue de la Paix, Nantes', N'admin@ecorider.io', 1);
SET IDENTITY_INSERT [Organisations] OFF;

-- 2. USERS (Note: LasttName avec deux 't' selon ton snapshot)
SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [OrganisationId], [FirstName], [LasttName], [Email], [Hpassword], [Role], [TailleCm], [IsActif]) VALUES 
(1, 1, N'Yass', N'Admin', N'yass@mojo.com', N'HASH_P1', 1, 180.5, 1),
(2, 2, N'Alice', N'Martin', N'alice.m@vls.fr', N'HASH_P2', 1, 165.0, 1),
(3, 2, N'Luc', N'Bernard', N'luc.b@vls.fr', N'HASH_P3', 0, 182.0, 1),
(4, 3, N'Chloé', N'Petit', N'chloe.p@eco.io', N'HASH_P4', 0, 170.0, 1),
(5, 3, N'Thomas', N'Durand', N't.durand@eco.io', N'HASH_P5', 0, 185.0, 1),
(6, 1, N'Julie', N'Lefebvre', N'j.lefebvre@mojo.com', N'HASH_P6', 0, 162.0, 1);
SET IDENTITY_INSERT [Users] OFF;

-- 3. VELOS (Status: 1=Dispo, 0=Indispo)
SET IDENTITY_INSERT [Velos] ON;
INSERT INTO [Velos] ([Id], [NumeroSerie], [Marque], [Modele], [PrixAchat], [Status]) VALUES 
(1, N'SN-MOUST-001', N'Moustache', N'Lundi 27', 2800.00, 1),
(2, N'SN-MOUST-002', N'Moustache', N'Samedi 28', 3100.00, 1),
(3, N'SN-CANON-003', N'Cannondale', N'Synapse', 1800.00, 1),
(4, N'SN-BROMP-004', N'Brompton', N'C-Line', 1600.00, 1),
(5, N'SN-SPECI-005', N'Specialized', N'Turbo Vado', 3500.00, 1),
(6, N'SN-GIANT-006', N'Giant', N'Explore E+', 2400.00, 0);
SET IDENTITY_INSERT [Velos] OFF;

-- 4. ACCESSOIRES
SET IDENTITY_INSERT [Accessoires] ON;
INSERT INTO [Accessoires] ([Id], [Name], [Price], [Stock]) VALUES 
(1, N'Casque City Reflect', 25.00, 100),
(2, N'Antivol U Kryptonite', 45.00, 50),
(3, N'Panier Avant Amovible', 15.00, 30),
(4, N'Kit Lumieres LED USB', 12.00, 80);
SET IDENTITY_INSERT [Accessoires] OFF;

-- 5. CONTRATS (UserRhId avec h minuscule)
SET IDENTITY_INSERT [Contrats] ON;
INSERT INTO [Contrats] ([Id], [UserRhId], [BeneficiaireId], [VeloId], [DateDebut], [DateFin], [LoyerMensuelHT], [StatutContrat]) VALUES 
(1, 1, 3, 1, '2025-01-01', '2025-12-31', 95.00, 1),
(2, 1, 4, 4, '2025-02-01', '2026-01-31', 75.00, 1),
(3, 2, 5, 2, '2025-03-01', '2026-02-28', 110.00, 1),
(4, 1, 6, 5, '2025-04-01', '2026-03-31', 130.00, 1),
(5, 2, 3, 3, '2025-05-01', '2026-04-30', 85.00, 1);
SET IDENTITY_INSERT [Contrats] OFF;

-- 6. RELATION MANY-TO-MANY (Table AccessoireContrat)
INSERT INTO [AccessoireContrat] ([AccessoiresId], [ContratsId]) VALUES 
(1, 1), (2, 1), -- Contrat 1: Casque + Antivol
(1, 2), (3, 2), -- Contrat 2: Casque + Panier
(4, 3),         -- Contrat 3: Kit Lumieres
(1, 4), (2, 4), -- Contrat 4: Casque + Antivol
(3, 5);         -- Contrat 5: Panier

-- 7. AMORTISSEMENTS
SET IDENTITY_INSERT [Amortissements] ON;
INSERT INTO [Amortissements] ([Id], [VeloId], [DateDebut], [DureeMois], [ValeurInit], [ValeurResiduelleFinale]) VALUES 
(1, 1, '2025-01-01', 36, 2800.00, 420.00),
(2, 2, '2025-03-01', 36, 3100.00, 465.00),
(3, 3, '2025-05-01', 36, 1800.00, 270.00),
(4, 4, '2025-02-01', 24, 1600.00, 320.00),
(5, 5, '2025-04-01', 48, 3500.00, 500.00);
SET IDENTITY_INSERT [Amortissements] OFF;

-- 8. INTERVENTIONS
SET IDENTITY_INSERT [Interventions] ON;
INSERT INTO [Interventions] ([Id], [VeloId], [DateIntervention], [TypeIntervention], [Description], [Cout]) VALUES 
(1, 1, '2025-06-15', N'Révision', N'Contrôle annuel complet', 120.00),
(2, 3, '2025-07-20', N'Réparation', N'Changement de chaîne', 85.50),
(3, 5, '2025-08-05', N'Maintenance', N'Mise à jour moteur', 45.00);
SET IDENTITY_INSERT [Interventions] OFF;

-- 9. DISCUSSIONS
SET IDENTITY_INSERT [Discussions] ON;
INSERT INTO [Discussions] ([Id], [UserId], [Objet], [DateCreation], [Status]) VALUES 
(1, 3, N'Problème de charge batterie', '2025-06-10 10:00:00', 1),
(2, 4, N'Question assurance vol', '2025-07-01 14:30:00', 1),
(3, 6, N'Accès local vélo', '2025-08-15 09:15:00', 0);
SET IDENTITY_INSERT [Discussions] OFF;

-- 10. MESSAGES
SET IDENTITY_INSERT [Messages] ON;
INSERT INTO [Messages] ([Id], [DiscussionId], [UserId], [Contenu], [DateEnvoi]) VALUES 
(1, 1, 3, N'Ma batterie ne charge plus au dessus de 80%.', '2025-06-10 10:05:00'),
(2, 1, 1, N'Nous avons pris rendez-vous pour un diagnostic.', '2025-06-10 11:30:00'),
(3, 2, 4, N'Quel antivol est recommandé par Mojo ?', '2025-07-01 14:35:00'),
(4, 2, 1, N'L''antivol U fourni dans votre pack accessoire.', '2025-07-01 15:10:00');
SET IDENTITY_INSERT [Messages] OFF;

COMMIT;